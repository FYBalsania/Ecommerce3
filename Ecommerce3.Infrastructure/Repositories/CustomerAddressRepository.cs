using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal class CustomerAddressRepository : Repository<CustomerAddress>, ICustomerAddressRepository
{
    private readonly AppDbContext _dbContext;

    public CustomerAddressRepository(AppDbContext dbContext) : base(dbContext) => _dbContext = dbContext;
}