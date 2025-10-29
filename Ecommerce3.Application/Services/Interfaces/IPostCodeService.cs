using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.PostCode;
using Ecommerce3.Contracts.DTOs.PostCode;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IPostCodeService
{
    Task<PagedResult<PostCodeListItemDTO>> GetListItemsAsync(PostCodeFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    Task AddAsync(AddPostCodeCommand command, CancellationToken cancellationToken);
    Task<PostCodeDTO?> GetByPostCodeIdAsync(int id, CancellationToken cancellationToken);
    Task EditAsync(EditPostCodeCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
}