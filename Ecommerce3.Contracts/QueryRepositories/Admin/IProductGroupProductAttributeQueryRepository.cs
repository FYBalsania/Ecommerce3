namespace Ecommerce3.Contracts.QueryRepositories.Admin;

public interface IProductGroupProductAttributeQueryRepository
{
    Task<decimal> GetMaxSortOrderAsync(int productGroupId, CancellationToken cancellationToken);
}