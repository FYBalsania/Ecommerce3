namespace Ecommerce3.Application.Services.Admin.Interfaces;

public interface IProductGroupProductAttributeService
{
    Task<decimal> GetMaxSortOrderAsync(int productGroupId, CancellationToken cancellationToken);
}