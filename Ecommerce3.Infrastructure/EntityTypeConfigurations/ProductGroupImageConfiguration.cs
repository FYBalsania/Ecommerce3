using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public sealed class ProductGroupImageConfiguration : IEntityTypeConfiguration<ProductGroupImage>
{
    public void Configure(EntityTypeBuilder<ProductGroupImage> builder)
    {
        //Properties.
        builder.Property(x => x.ProductGroupId).HasColumnType("integer").HasColumnOrder(16);
        
        //Relations.
        builder.HasOne(x => x.ProductGroup)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.ProductGroupId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}