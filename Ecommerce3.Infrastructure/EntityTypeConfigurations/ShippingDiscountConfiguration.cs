using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ShippingDiscountConfiguration : IEntityTypeConfiguration<ShippingDiscount>
{
    public void Configure(EntityTypeBuilder<ShippingDiscount> builder)
    {
        
    }
}