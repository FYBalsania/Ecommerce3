using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Repositories;
using Ecommerce3.Infrastructure.Data;

namespace Ecommerce3.Infrastructure.Repositories;

internal sealed class CustomerAddressRepository : Repository<CustomerAddress>, ICustomerAddressRepository
{
    public CustomerAddressRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}