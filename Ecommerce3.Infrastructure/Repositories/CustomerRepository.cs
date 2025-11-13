using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    public CustomerRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<(IEnumerable<Customer> ListItems, int Count)?> GetCustomersAsync(string? firstName,
        string? lastName, string? companyName, string? emailAddress, string? phoneNumber, CustomerInclude includes,
        bool trackChanges, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByEmailAsync(string email, int? excludeId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer?> GetByEmailAsync(string email, CustomerInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Customer?> GetByIdAsync(string email, CustomerInclude includes, bool trackChanges,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}