using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    private readonly AppDbContext _dbContext;

    public CustomerRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;

    public Task<(IEnumerable<Customer> ListItems, int Count)?> GetCustomersAsync(string? firstName,
        string? lastName, string? companyName, string? emailAddress, string? phoneNumber, int pageNumber, int pageSize,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}