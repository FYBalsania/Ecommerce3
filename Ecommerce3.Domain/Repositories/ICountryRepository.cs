using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface ICountryRepository
{
    Task<Country?> GetByIdAsync(int id, CountryInclude includes, bool trackChanges,
        CancellationToken cancellationToken);
}