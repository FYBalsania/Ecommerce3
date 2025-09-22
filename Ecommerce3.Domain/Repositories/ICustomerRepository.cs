using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    public Task<(IEnumerable<CustomerListItem> ListItems, int Count)?> GetCustomerListItemsAsync(string? firstName,
        string? lastName, string? companyName, string? emailAddress, string? phoneNumber, int pageNumber, int pageSize,
        CancellationToken cancellationToken);

    public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
    public Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}