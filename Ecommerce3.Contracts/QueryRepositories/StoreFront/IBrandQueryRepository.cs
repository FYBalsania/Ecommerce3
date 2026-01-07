namespace Ecommerce3.Contracts.QueryRepositories.StoreFront;

public interface IBrandQueryRepository
{
    Task<IReadOnlyDictionary<int, string>> GetIdAndDisplayByCategoryIdsAsync(int[] categoryIds,
        CancellationToken cancellationToken);
}