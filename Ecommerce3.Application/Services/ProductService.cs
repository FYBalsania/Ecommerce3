using Ecommerce3.Application.Commands.Product;
using Ecommerce3.Application.Services.Interfaces;

namespace Ecommerce3.Application.Services;

public class ProductService : IProductService
{
    public async Task AddAsync(AddProductCommand command, CancellationToken cancellationToken)
    {
    }

    public async Task EditAsync(EditProductCommand command, CancellationToken cancellationToken)
    {
    }
}