using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.Page;
using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IPageService
{
    Task<PagedResult<PageListItemDTO>> GetListItemsAsync(PageFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task AddAsync(AddPageCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditPageCommand command, CancellationToken cancellationToken);
    Task<PageDTO?> GetByIdAsync(int id, Type entity, CancellationToken cancellationToken);
}