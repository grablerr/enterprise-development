using AutoMapper;
using EstateAgency.Contracts.Dtos;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using EstateAgency.Infrastructure.Repositories;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace EstateAgency.RabbitMqConsumer;

public class RabbitMqConsumer(
    IConnectionFactory connectionFactory,
    ILogger<RabbitMqConsumer> logger,
    IMapper mapper,
    IServiceScopeFactory scopeFactory) : BackgroundService
{
    private const string ExchangeName = "data-exchange";
    private const string QueueName = "data-queue";

    private async Task<IConnection> ConnectWithRetryAsync(
        CancellationToken stoppingToken,
        int maxRetries = 5,
        int delayMs = 1000)
    {
        var attempt = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                attempt++;
                var connection = await connectionFactory.CreateConnectionAsync(stoppingToken);
                logger.LogInformation("Connected to RabbitMQ on attempt {Attempt}", attempt);
                return connection;
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Failed to connect to RabbitMQ on attempt {Attempt}", attempt);
                if (attempt >= maxRetries)
                {
                    logger.LogError("Max retries ({MaxRetries}) reached. Throwing.", maxRetries);
                    throw;
                }
                await Task.Delay(delayMs, stoppingToken);
            }
        }
        throw new OperationCanceledException("Connection attempts cancelled");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await using var connection = await ConnectWithRetryAsync(stoppingToken);
            await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

            await channel.ExchangeDeclareAsync(
                exchange: ExchangeName,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                cancellationToken: stoppingToken);

            await channel.QueueDeclareAsync(
                queue: QueueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                cancellationToken: stoppingToken);

            var routingKeys = new[] { "counterparty.create", "realestate.create", "application.create" };
            foreach (var routingKey in routingKeys)
            {
                await channel.QueueBindAsync(QueueName, ExchangeName, routingKey, cancellationToken: stoppingToken);
            }

            await channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false, cancellationToken: stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (sender, ea) =>
            {
                try
                {
                    var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var routingKey = ea.RoutingKey;

                    logger.LogInformation("Received message. RoutingKey: {RoutingKey}, Payload: {Json}", routingKey, json);
                    await ProcessMessageAsync(routingKey, json);

                    await channel.BasicAckAsync(
                        ea.DeliveryTag,
                        multiple: false,
                        cancellationToken: stoppingToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error processing message with routing key {RoutingKey}", ea.RoutingKey);
                    await channel.BasicNackAsync(ea.DeliveryTag,
                        multiple: false,
                        requeue: false,
                        cancellationToken: stoppingToken);
                }
            };

            await channel.BasicConsumeAsync(queue: QueueName,
                autoAck: false,
                consumer: consumer,
                cancellationToken: stoppingToken);

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            logger.LogCritical(ex, "RabbitMQ consumer failed to start");
            throw;
        }
    }

    private async Task ProcessMessageAsync(string routingKey, string json)
    {
        try
        {
            using var scope = scopeFactory.CreateScope();
            var counterpartyRepo = scope.ServiceProvider.GetRequiredService<IRepository<Counterparty>>();
            var realEstateRepo = scope.ServiceProvider.GetRequiredService<IRepository<RealEstate>>();
            var applicationRepo = scope.ServiceProvider.GetRequiredService<IRepository<Application>>();

            switch (routingKey)
            {
                case "counterparty.create":
                    await ProcessCounterpartyMessageAsync(json, counterpartyRepo);
                    break;
                case "realestate.create":
                    await ProcessRealEstateMessageAsync(json, realEstateRepo);
                    break;
                case "application.create":
                    await ProcessApplicationMessageAsync(json, applicationRepo);
                    break;
                default:
                    logger.LogWarning("Unknown routing key received: {RoutingKey}", routingKey);
                    break;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error in ProcessMessageAsync for routing key: {RoutingKey}", routingKey);
            throw;
        }
    }


    private async Task ProcessCounterpartyMessageAsync(string json, IRepository<Counterparty> repo)
    {
        var dto = JsonSerializer.Deserialize<CounterpartyCreateDto>(json);

        if (dto == null)
        {
            logger.LogWarning("Invalid CounterpartyCreateDto JSON: {Json}", json);
            return;
        }

        var entity = mapper.Map<Counterparty>(dto);
        await repo.AddAsync(entity);
        logger.LogInformation("Counterparty saved with ID {Id}", entity.Id);
    }

    private async Task ProcessRealEstateMessageAsync(string json, IRepository<RealEstate> repo)
    {
        var dto = JsonSerializer.Deserialize<RealEstateCreateDto>(json);

        if (dto == null)
        {
            logger.LogWarning("Invalid RealEstateCreateDto JSON: {Json}", json);
            return;
        }

        var entity = mapper.Map<RealEstate>(dto);
        await repo.AddAsync(entity);
        logger.LogInformation("RealEstate saved with ID {Id}", entity.Id);
    }

    private async Task ProcessApplicationMessageAsync(string json, IRepository<Application> repo)
    {
        var dto = JsonSerializer.Deserialize<ApplicationCreateDto>(json);

        if (dto == null)
        {
            logger.LogWarning("Invalid ApplicationCreateDto JSON: {Json}", json);
            return;
        }

        var entity = mapper.Map<Application>(dto);
        await repo.AddAsync(entity);
        logger.LogInformation("Application saved with ID {Id}", entity.Id);
    }
}