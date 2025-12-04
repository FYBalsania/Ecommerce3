using Ecommerce3.Application.Commands.UnitOfMeasure;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IUnitOfMeasureService
{
    public Task AddAsync(AddUnitOfMeasureCommand command, CancellationToken cancellationToken);
    public Task EditAsync(EditUnitOfMeasureCommand command, CancellationToken cancellationToken);
    public Task DeleteAsync(DeleteUnitOfMeasureCommand command, CancellationToken cancellationToken);
}