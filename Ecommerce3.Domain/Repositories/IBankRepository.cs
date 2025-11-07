using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IBankRepository : IRepository<Bank> 
{
    public Task<Bank?> GetByIdAsync(int id, BankInclude includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<Bank?> GetByNameAsync(string name, BankInclude includes, bool trackChanges,
        CancellationToken cancellationToken);
}