using EstateAgency.Application.AnalyticService;
using EstateAgency.Application.Mapper;
using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using EstateAgency.Infrastructure.Persistence;
using EstateAgency.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

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
    var xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml");
    foreach (var xmlFile in xmlFiles)
        c.IncludeXmlComments(xmlFile);
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