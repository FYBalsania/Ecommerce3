using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Brand;
using Ecommerce3.Contracts.DTOs.Brand;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IBrandService
{
    Task<PagedResult<BrandListItemDTO>> GetListItemsAsync(BrandFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    Task AddAsync(AddBrandCommand command, CancellationToken cancellationToken);
    Task<BrandDTO?> GetByBrandIdAsync(int id, CancellationToken cancellationToken);
    Task EditAsync(EditBrandCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task<int> GetMaxSortOrderAsync(CancellationToken cancellationToken);
    Task<IDictionary<int, string>> GetIdAndNameListAsync(CancellationToken cancellationToken);
}