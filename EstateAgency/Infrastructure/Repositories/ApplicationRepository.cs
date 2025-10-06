using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories;
public class ApplicationRepository(AppDbContext context) : IApplicationRepository
{
    public async Task AddAsync(Application application)
    {
        if (application == null) throw new ArgumentNullException(nameof(application));

        await context.Applications.AddAsync(application);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var application = await context.Applications.FindAsync(id);
        if (application == null) throw new KeyNotFoundException($"Estate with Id {id} not found.");

        context.Applications.Remove(application);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Application>> GetAllAsync() =>
        await context.Applications.ToListAsync();

    public async Task<Application> GetByIdAsync(int id) =>
        await context.Applications.FindAsync(id);

    public async Task<bool> IsExistsAsync(int id) =>
        await context.Applications.AnyAsync(a => a.Id == id);

    public async Task UpdateAsync(Application application)
    {
        if (application == null) throw new ArgumentNullException(nameof(application));
        var toUpdate = await context.Applications.FindAsync(application.Id);
        if (toUpdate == null) throw new KeyNotFoundException($"Estate with Id {application.Id} not found.");

        toUpdate.CounterpartyId = application.CounterpartyId;
        toUpdate.Counterparty = application.Counterparty;
        toUpdate.RealEstateId = application.RealEstateId;
        toUpdate.RealEstate = application.RealEstate;
        toUpdate.TransactionAmount = application.TransactionAmount;
        toUpdate.Type = application.Type;
        toUpdate.Date = application.Date;

        context.Applications.Update(toUpdate);
        await context.SaveChangesAsync();
    }
}
