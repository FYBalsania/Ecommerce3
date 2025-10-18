using Ecommerce3.Application.Commands.ProductGroup;
using Ecommerce3.Application.Services.Interfaces;

namespace Ecommerce3.Application.Services;

public class ProductGroupService : IProductGroupService
{
    public async Task AddAsync(AddProductGroupCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task EditAsync(EditProductGroupCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}