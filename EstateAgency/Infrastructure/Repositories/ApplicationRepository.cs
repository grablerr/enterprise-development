using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;
public class ApplicationRepository(AppDbContext context) : IApplicationRepository
{
    public Task<Application> AddAsync(Application application)
    {
        throw new NotImplementedException();
    }

    public Task<Application> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Application>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Application> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Application> IsExistsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Application> UpdateAsync(Application application)
    {
        throw new NotImplementedException();
    }
}
