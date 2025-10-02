using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;
public class RealEstateRepository(AppDbContext context) : IRealEstateRepository
{
    public async Task<RealEstate> AddAsync(RealEstate realEstate)
    {
        throw new NotImplementedException();
    }

    public Task<RealEstate> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RealEstate>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<RealEstate> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<RealEstate> IsExistsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<RealEstate> UpdateAsync(RealEstate realEstate)
    {
        throw new NotImplementedException();
    }
}
