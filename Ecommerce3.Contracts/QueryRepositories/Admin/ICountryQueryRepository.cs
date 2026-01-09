namespace Ecommerce3.Contracts.QueryRepositories.Admin;

public interface ICountryQueryRepository
{
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    Task<bool> ExistsByIso2CodeAsync(string iso2Code, int? excludeId, CancellationToken cancellationToken);
    Task<bool> ExistsByIso3CodeAsync(string iso3Code, int? excludeId, CancellationToken cancellationToken);
    Task<bool> ExistsByNumericCodeAsync(string numericCode, int? excludeId, CancellationToken cancellationToken);
}