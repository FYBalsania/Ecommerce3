using Ecommerce3.Application.Commands;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IBrandService
{
    Task AddBrandAsync(AddBrandCommand command, CancellationToken cancellationToken);
    Task UpdateBrandAsync(UpdateBrandCommand command, CancellationToken cancellationToken);
    Task DeleteBrandAsync(int id, CancellationToken cancellationToken);
}