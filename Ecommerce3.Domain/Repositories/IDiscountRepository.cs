using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface IDiscountRepository : IRepository<Discount>
{
    public Task<(IEnumerable<Discount> ListItems, int Count)> GetDiscountsAsync(string? scope,
        string? code, string? name, DiscountInclude[] includes, bool trackChanges, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    public Task<bool> ExistsByNameAsync(string name, int? excludeId, CancellationToken cancellationToken);
    public Task<bool> ExistsByCodeAsync(string code, int? excludeId, CancellationToken cancellationToken);

    public Task<Discount?> GetByIdAsync(int id, DiscountInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<Discount?> GetByCodeAsync(string code, DiscountInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<Discount?> GetByNameAsync(string name, DiscountInclude[] includes, bool trackChanges,
        CancellationToken cancellationToken);
}