using Ecommerce3.Application.Commands.UnitOfMeasure;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IUnitOfMeasureService
{
    Task AddAsync(AddUnitOfMeasureCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditUnitOfMeasureCommand command, CancellationToken cancellationToken);
    Task DeleteAsync(DeleteUnitOfMeasureCommand command, CancellationToken cancellationToken);
    Task<IDictionary<int, string>> GetIdAndNameDictionaryAsync(CancellationToken cancellationToken);
}