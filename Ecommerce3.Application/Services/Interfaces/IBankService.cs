using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Bank;
using Ecommerce3.Contracts.DTOs.Bank;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IBankService
{
    Task<PagedResult<BankListItemDTO>> GetListItemsAsync(BankFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    Task AddAsync(AddBankCommand command, CancellationToken cancellationToken);
    Task<BankDTO?> GetByBankIdAsync(int id, CancellationToken cancellationToken);
    Task EditAsync(EditBankCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
}