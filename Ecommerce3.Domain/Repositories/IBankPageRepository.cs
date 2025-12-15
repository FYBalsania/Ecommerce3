using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IBankPageRepository : IPageRepository<BankPage>
{
    Task<BankPage?> GetByBankIdAsync(int bankId, BankPageInclude include, bool trackChanges,
        CancellationToken cancellationToken);
}