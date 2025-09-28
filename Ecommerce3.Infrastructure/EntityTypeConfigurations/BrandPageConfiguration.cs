using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class BrandPageConfiguration : IEntityTypeConfiguration<BrandPage>
{
    public void Configure(EntityTypeBuilder<BrandPage> builder)
    {
        // Map FK to existing column
        builder.Property(x => x.BrandId).HasColumnName(nameof(BrandPage.BrandId));

        //Relations.
        builder.HasOne(x => x.Brand)
            .WithOne()
            .HasForeignKey<BrandPage>(x => x.BrandId)
            .HasPrincipalKey<Brand>(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}