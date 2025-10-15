using EstateAgency.Application.AnalyticService;
using EstateAgency.Application.Mapper;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using EstateAgency.Infrastructure.Persistence;
using EstateAgency.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

/// <summary>
/// Application setup for dependency injection, database context, services, repositories,
/// middleware configuration, and API endpoints including Swagger support.
/// </summary>
var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddMySqlDbContext<AppDbContext>(connectionName: "DefaultConnection");

builder.Services.AddAutoMapper(typeof(AppMapper).Assembly);

builder.Services.AddScoped<IRepository<Counterparty>, CounterpartyRepository>();
builder.Services.AddScoped<IRepository<RealEstate>, RealEstateRepository>();
builder.Services.AddScoped<IRepository<Application>, ApplicationRepository>();

builder.Services.AddScoped<AnalyticsService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.MapDefaultEndpoints();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    await DbSeeder.SeedAsync(db);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapDefaultEndpoints();
app.MapControllers();

app.Run();