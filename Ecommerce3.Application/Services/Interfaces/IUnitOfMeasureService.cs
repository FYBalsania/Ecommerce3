using cloudscribe.Pagination.Models;
using Ecommerce3.Application.Commands.UnitOfMeasure;
using Ecommerce3.Contracts.DTOs.UnitOfMeasure;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IUnitOfMeasureService
{
    Task<PagedResult<UnitOfMeasureListItemDTO>> GetListItemsAsync(UnitOfMeasureFilter filter, int pageNumber, int pageSize,
        CancellationToken cancellationToken);
    Task AddAsync(AddUnitOfMeasureCommand command, CancellationToken cancellationToken);
    Task<UnitOfMeasureDTO?> GetByUnitOfMeasureIdAsync(int id, CancellationToken cancellationToken);
    Task EditAsync(EditUnitOfMeasureCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteUnitOfMeasureCommand command, CancellationToken cancellationToken);
    Task<IDictionary<int, string>> GetIdAndNameDictionaryAsync(int? excludeId = null, CancellationToken cancellationToken = default);
}