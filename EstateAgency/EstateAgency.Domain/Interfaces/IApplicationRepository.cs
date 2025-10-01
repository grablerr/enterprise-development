using EstateAgency.Domain.Entities;

namespace EstateAgency.Domain.Interfaces;
public interface IApplicationRepository
{
    public Task<Application> AddAsync(Application application);
    public Task<IEnumerable<Application>> GetAllAsync();
    public Task<Application> GetByIdAsync(int id);
    public Task<Application> UpdateAsync(Application application);
    public Task<Application> DeleteAsync(int id);
    public Task<Application> IsExistsAsync(int id);
}
