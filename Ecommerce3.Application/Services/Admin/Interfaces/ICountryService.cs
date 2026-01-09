using Ecommerce3.Application.Commands.Admin.Country;

namespace Ecommerce3.Application.Services.Admin.Interfaces;

public interface ICountryService
{
    Task AddAsync(AddCountryCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditCountryCommand command, CancellationToken cancellationToken);   
}