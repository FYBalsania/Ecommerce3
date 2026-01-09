using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class CountryRepository(AppDbContext dbContext) : ICountryRepository
{
    public async Task<Country?> GetByIdAsync(int id, CountryInclude includes, bool trackChanges, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}