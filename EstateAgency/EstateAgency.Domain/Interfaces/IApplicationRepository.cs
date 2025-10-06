using EstateAgency.Domain.Entities;

namespace EstateAgency.Domain.Interfaces;
public interface IApplicationRepository
{
    public Task AddAsync(Application application);
    public Task DeleteAsync(int id);
    public Task<IEnumerable<Application>> GetAllAsync();
    public Task<Application> GetByIdAsync(int id);
    public Task<bool> IsExistsAsync(int id);
    public Task UpdateAsync(Application application);
}
