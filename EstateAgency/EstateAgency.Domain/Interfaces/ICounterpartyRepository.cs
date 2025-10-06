using EstateAgency.Domain.Entities;

namespace EstateAgency.Domain.Interfaces;
public interface ICounterpartyRepository
{
    public Task AddAsync(Counterparty counterparty);
    public Task DeleteAsync(int id);
    public Task<IEnumerable<Counterparty>> GetAllAsync();
    public Task<Counterparty> GetByIdAsync(int id);
    public Task<bool> IsExistsAsync(int id);
    public Task UpdateAsync(Counterparty counterparty);
}