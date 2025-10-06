using EstateAgency.Domain.Entities;

namespace EstateAgency.Domain.Interfaces;
public interface IRealEstateRepository
{
    public Task AddAsync(RealEstate realEstate);
    public Task DeleteAsync(int id);
    public Task<IEnumerable<RealEstate>> GetAllAsync();
    public Task<RealEstate> GetByIdAsync(int id);
    public Task<bool> IsExistsAsync(int id);
    public Task UpdateAsync(RealEstate realEstate);
}
