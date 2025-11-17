using EstateAgency.Contracts.Mapper;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using EstateAgency.Infrastructure.Persistence;
using EstateAgency.Infrastructure.Repositories;
using EstateAgency.RabbitMqConsumer;
using RabbitMQ.Client;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

builder.AddMySqlDbContext<AppDbContext>(connectionName: "DefaultConnection");

builder.Services.AddScoped<IRepository<Counterparty>, CounterpartyRepository>();
builder.Services.AddScoped<IRepository<RealEstate>, RealEstateRepository>();
builder.Services.AddScoped<IRepository<Application>, ApplicationRepository>();

builder.Services.AddAutoMapper(typeof(AppMapper).Assembly);

builder.Services.AddSingleton<IConnectionFactory>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("RabbitMQ");

    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new InvalidOperationException("RabbitMQ connection string is not configured");
    }

    return new ConnectionFactory
    {
        Uri = new Uri(connectionString),
    };
});

builder.Services.AddHostedService<RabbitMqConsumer>();

var host = builder.Build();

host.Run();