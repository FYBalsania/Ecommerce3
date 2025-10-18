namespace Ecommerce3.Contracts.QueryRepositories;

public interface IProductAttributeQueryRepository
{
    public Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    public Task<bool> ExistsBySlugAsync(string slug, int? excludeId, CancellationToken cancellationToken);
}