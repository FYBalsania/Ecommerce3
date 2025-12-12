namespace Ecommerce3.Contracts.QueryRepositories;

public interface IUnitOfMeasureQueryRepository
{
    Task<bool> ExistsByCodeAsync(string code, int? excludeId, CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    Task<IDictionary<int, string>> GetIdAndNameDictionaryAsync(CancellationToken cancellationToken);
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken);
}