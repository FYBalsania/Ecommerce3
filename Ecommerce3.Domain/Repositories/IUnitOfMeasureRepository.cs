using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface IUnitOfMeasureRepository : IRepository<UnitOfMeasure>
{
    public Task<UnitOfMeasure?> GetByIdAsync(int id, UnitOfMeasureInclude includes, bool trackChanges,
        CancellationToken cancellationToken);
}