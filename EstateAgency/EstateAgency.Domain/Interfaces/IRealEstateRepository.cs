using EstateAgency.Domain.Entities;

namespace EstateAgency.Domain.Interfaces;
public interface IRealEstateRepository
{
    public Task<RealEstate> AddAsync(RealEstate realEstate);
    public Task<RealEstate> DeleteAsync(int id);
    public Task<IEnumerable<RealEstate>> GetAllAsync();
    public Task<RealEstate> GetByIdAsync(int id);
    public Task<RealEstate> IsExistsAsync(int id);
    public Task<RealEstate> UpdateAsync(RealEstate realEstate);
}
