using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.ImageType;
using Ecommerce3.Contracts.DTOs.ImageType;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IImageTypeService
{
    Task<PagedResult<ImageTypeListItemDTO>> GetListItemsAsync(ImageTypeFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    Task AddAsync(AddImageTypeCommand command, CancellationToken cancellationToken);
    Task<ImageTypeDTO?> GetByImageTypeIdAsync(int id, CancellationToken cancellationToken);
    Task EditAsync(EditImageTypeCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(int id, CancellationToken cancellationToken);
    Task<Dictionary<int, string>> GetIdAndNamesByEntityAsync(string entity, CancellationToken cancellationToken);
}