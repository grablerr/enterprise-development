using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;
public class RealEstateRepository(AppDbContext context) : IRealEstateRepository
{
    public async Task AddAsync(RealEstate realEstate)
    {
        if (realEstate == null) throw new ArgumentNullException(nameof(realEstate));

        await context.RealEstates.AddAsync(realEstate);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var realEstate = await context.RealEstates.FindAsync(id);
        if (realEstate == null) throw new KeyNotFoundException($"Estate with Id {id} not found.");

        context.RealEstates.Remove(realEstate);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<RealEstate>> GetAllAsync() =>
        await context.RealEstates.ToListAsync();

    public async Task<RealEstate> GetByIdAsync(int id) =>
        await context.RealEstates.FindAsync(id);

    public async Task<bool> IsExistsAsync(int id) =>
        await context.RealEstates.AnyAsync(e => e.Id == id);

    public async Task UpdateAsync(RealEstate realEstate)
    {
        if (realEstate == null) throw new ArgumentNullException(nameof(realEstate));
        var toUpdate = await context.RealEstates.FindAsync(realEstate.Id);
        if (toUpdate == null) throw new KeyNotFoundException($"Estate with Id {realEstate.Id} not found.");

        toUpdate.Type = realEstate.Type;
        toUpdate.Purpose = realEstate.Purpose;
        toUpdate.CadastralNumber = realEstate.CadastralNumber;
        toUpdate.Address = realEstate.Address;
        toUpdate.FloorNumber = realEstate.FloorNumber;  
        toUpdate.Floors = realEstate.Floors;
        toUpdate.Square = realEstate.Square;
        toUpdate.Rooms = realEstate.Rooms;
        toUpdate.CeilingHeight = realEstate.CeilingHeight;
        toUpdate.IsEncumbrance = realEstate.IsEncumbrance;

        context.RealEstates.Update(toUpdate);
        await context.SaveChangesAsync();
    }
}
