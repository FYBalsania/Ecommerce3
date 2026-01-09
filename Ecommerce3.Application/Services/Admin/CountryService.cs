using Ecommerce3.Application.Commands.Admin.Country;
using Ecommerce3.Application.Services.Admin.Interfaces;
using Ecommerce3.Contracts.QueryRepositories.Admin;
using Ecommerce3.Domain.Repositories;

namespace Ecommerce3.Application.Services.Admin;

internal sealed class CountryService(ICountryRepository repository,
    ICountryQueryRepository queryRepository) : ICountryService
{
    public async Task AddAsync(AddCountryCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task EditAsync(EditCountryCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}