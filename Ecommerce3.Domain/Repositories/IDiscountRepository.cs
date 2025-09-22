using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IDiscountRepository : IRepository<Discount>
{
    public Task<(IEnumerable<DiscountListItem> ListItems, int Count)> GetDiscountListItemsAsync(string? scope,
        string? code, string? name, int pageNumber, int pageSize, CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
    public Task<bool> ExistsByCodeAsync(string code, CancellationToken cancellationToken);
    public Task<Discount?> GetByCodeAsync(string code, CancellationToken cancellationToken);
    public Task<Discount?> GetByNameAsync(string name, CancellationToken cancellationToken);
}