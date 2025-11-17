using EstateAgency.DataGenerator;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace EstateAgency.RabbitMqProducer;

/// <summary>
/// RabbitMQ producer service that generates and publishes real estate agency data contracts.
/// </summary>
/// <param name="connectionFactory">Factory for creating RabbitMQ connections.</param>
/// <param name="logger">Logger instance for recording publishing events and errors.</param>
/// <param name="configuration">Configuration provider to access settings like publish delay.</param>
public class RabbitMqProducer(
IConnectionFactory connectionFactory,
ILogger<RabbitMqProducer> logger,
IConfiguration configuration) : BackgroundService
{
    /// <summary>
    /// Initial count for generating bogus real estate records.
    /// </summary>
    private const int RealEstatesCount = 1;


    /// <summary>
    /// Initial count for generating bogus counterparty records.
    /// </summary>
    private const int CounterpartiesCount = 1;

    /// <summary>
    /// Data generator producing real estate, counterparty, and application contracts.
    /// </summary>
    private readonly BogusGenerator _generator = new(RealEstatesCount, CounterpartiesCount);

    /// <summary>
    /// Name of the RabbitMQ exchange to publish messages to.
    /// </summary>
    private const string ExchangeName = "data-exchange";

    /// <summary>
    /// Attempts to establish a RabbitMQ connection with retry logic on failure.
    /// </summary>
    /// <param name="stoppingToken">Token to signal cancellation of retries.</param>
    /// <param name="maxRetries">Maximum retry attempts before throwing an exception.</param>
    /// <param name="delayMs">Delay in milliseconds between retry attempts.</param>
    /// <returns>Established RabbitMQ connection.</returns>
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
                    logger.LogError("Max retry attempts reached ({MaxRetries}). Throwing.", maxRetries);
                    throw;
                }
                await Task.Delay(delayMs, stoppingToken);
            }
        }
        throw new OperationCanceledException("Connection attempts cancelled.");
    }

    /// <summary>
    /// Runs the producer service asynchronously, generating and publishing messages to RabbitMQ
    /// until the <paramref name="stoppingToken"/> signals cancellation.
    /// </summary>
    /// <param name="stoppingToken">Token to cancel the background service execution.</param>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await using var connection = await ConnectWithRetryAsync(stoppingToken);
            await using var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);

            var delayMs = configuration.GetValue<int>("RABBITMQ_PUBLISH_DELAY_MS", 100);

            await channel.ExchangeDeclareAsync(
                exchange: ExchangeName,
                type: ExchangeType.Direct,
                durable: true,
                autoDelete: false,
                cancellationToken: stoppingToken);

            var random = new Random();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var typeChoice = random.Next(3);
                    string routingKey;
                    object payload;

                    switch (typeChoice)
                    {
                        case 0:
                            routingKey = "counterparty.create";
                            payload = _generator.GenerateCounterparty();
                            break;
                        case 1:
                            routingKey = "realestate.create";
                            payload = _generator.GenerateRealEstate();
                            break;
                        case 2:
                            routingKey = "application.create";
                            payload = _generator.GenerateApplication();
                            break;
                        default:
                            routingKey = "data";
                            payload = "test";
                            break;
                    }

                    var json = JsonSerializer.Serialize(payload);
                    var body = Encoding.UTF8.GetBytes(json);

                    await channel.BasicPublishAsync(
                        exchange: ExchangeName,
                        routingKey: routingKey,
                        mandatory: false,
                        basicProperties: new BasicProperties { Persistent = true },
                        body: body,
                        cancellationToken: stoppingToken);

                    logger.LogInformation("Sent message. RoutingKey: {RoutingKey}, Type: {Type}",
                        routingKey, payload.GetType().Name);

                    await Task.Delay(delayMs, stoppingToken);
                }
                catch (Exception ex) when (ex is not OperationCanceledException)
                {
                    logger.LogError(ex, "Error sending message");
                    await Task.Delay(delayMs, stoppingToken);
                }
            }
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            throw;
        }
    }
}