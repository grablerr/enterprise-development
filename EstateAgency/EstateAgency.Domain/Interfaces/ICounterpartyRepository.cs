using EstateAgency.Domain.Entities;

namespace EstateAgency.Domain.Interfaces;
public interface ICounterpartyRepository
{
    public Task<Counterparty> AddAsync(Counterparty counterparty);
    public Task<Counterparty> DeleteAsync(int id);
    public Task<IEnumerable<Counterparty>> GetAllAsync();
    public Task<Counterparty> GetByIdAsync(int id);
    public Task<Counterparty> IsExistsAsync(int id);
    public Task<Counterparty> UpdateAsync(Counterparty counterparty);
}