using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    public Task<(IEnumerable<Customer> ListItems, int Count)?> GetCustomersAsync(string? firstName,
        string? lastName, string? companyName, string? emailAddress, string? phoneNumber, CustomerInclude includes,
        bool trackChanges, int pageNumber, int pageSize, CancellationToken cancellationToken);

    public Task<bool> ExistsByEmailAsync(string email, int? excludeId, CancellationToken cancellationToken);

    public Task<Customer?> GetByEmailAsync(string email, CustomerInclude includes, bool trackChanges,
        CancellationToken cancellationToken);

    public Task<Customer?> GetByIdAsync(string email, CustomerInclude includes, bool trackChanges,
        CancellationToken cancellationToken);
}