using EstateAgency.Domain.Entities;
using EstateAgency.Test.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

/// <summary>
/// Seeds the database with initial test data if the respective tables are empty.
/// Ensures data integrity by setting auto-increment values after insertion.
/// </summary>
public static class DbSeeder
{
    /// <summary>
    /// Seeds the RealEstates, Counterparties, and Applications tables with fixture data.
    /// </summary>
    /// <param name="context">Application database context</param>
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

    /// <summary>
    /// Returns the maximum Id value present in a list of entities.
    /// </summary>
    /// <typeparam name="T">Entity type with Id property</typeparam>
    /// <param name="list">List of entities</param>
    /// <returns>Maximum Id value or 0 if list is empty</returns>
    private static int GetMaxId<T>(List<T> list) where T : class
    {
        return list.Count == 0 ? 0 : (int)typeof(T).GetProperty("Id")!.GetValue(list.MaxBy(x => (int)typeof(T).GetProperty("Id")!.GetValue(x)))!;
    }
}