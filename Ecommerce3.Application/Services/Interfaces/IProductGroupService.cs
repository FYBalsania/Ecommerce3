using Ecommerce3.Application.Commands.ProductGroup;
using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Application.Services.Interfaces;

public interface IProductGroupService
{
    Task AddAsync(AddProductGroupCommand command, CancellationToken cancellationToken);
    Task EditAsync(EditProductGroupCommand command, CancellationToken cancellationToken);
}