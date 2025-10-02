using EstateAgency.Domain.Entities;
using EstateAgency.Domain.Interfaces;
using Infrastructure.Persistence;


namespace Infrastructure.Repositories;
public class CounterpartyRepository(AppDbContext context) : ICounterpartyRepository
{
    public Task<Counterparty> AddAsync(Counterparty counterparty)
    {
        throw new NotImplementedException();
    }

    public Task<Counterparty> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Counterparty>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Counterparty> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Counterparty> IsExistsAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Counterparty> UpdateAsync(Counterparty counterparty)
    {
        throw new NotImplementedException();
    }
}
