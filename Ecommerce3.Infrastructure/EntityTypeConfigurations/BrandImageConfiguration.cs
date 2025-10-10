using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public sealed class BrandImageConfiguration : IEntityTypeConfiguration<BrandImage>
{
    public void Configure(EntityTypeBuilder<BrandImage> builder)
    {
        //Properties.
        builder.Property(x => x.BrandId).HasColumnType("integer").HasColumnOrder(13);
        
        //Relations.
        builder.HasOne(x => x.Brand)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}