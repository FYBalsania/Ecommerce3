using Ecommerce3.Application.Commands.Product;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IProductService
{
    Task AddAsync(AddProductCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditProductCommand command, CancellationToken cancellationToken);
}