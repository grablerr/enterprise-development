using EstateAgency.Domain.Entities;
using EstateAgency.Test.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var fixture = new FixtureDataClass();

        if (!await context.RealEstates.AnyAsync())
        {
            await context.RealEstates.AddRangeAsync(fixture.EstateObjects);
            await context.SaveChangesAsync();
            await context.Database.ExecuteSqlAsync($"ALTER TABLE RealEstates AUTO_INCREMENT = {GetMaxId(fixture.EstateObjects) + 1};");
        }

        if (!await context.Counterparties.AnyAsync())
        {
            await context.Counterparties.AddRangeAsync(fixture.Counterparties);
            await context.SaveChangesAsync();
            await context.Database.ExecuteSqlAsync($"ALTER TABLE Counterparties AUTO_INCREMENT = {GetMaxId(fixture.Counterparties) + 1};");
        }

        if (!await context.Applications.AnyAsync())
        {
            await context.Applications.AddRangeAsync(fixture.Applications);
            await context.SaveChangesAsync();
            await context.Database.ExecuteSqlAsync($"ALTER TABLE Applications AUTO_INCREMENT = {GetMaxId(fixture.Applications) + 1};");
        }
    }

    private static int GetMaxId<T>(List<T> list) where T : class
    {
        return list.Count == 0 ? 0 : (int)typeof(T).GetProperty("Id")!.GetValue(list.MaxBy(x => (int)typeof(T).GetProperty("Id")!.GetValue(x)))!;
    }
}
