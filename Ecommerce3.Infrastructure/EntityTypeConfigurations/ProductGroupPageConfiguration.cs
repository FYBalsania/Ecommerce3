using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductGroupPageConfiguration : IEntityTypeConfiguration<ProductGroupPage>
{
    public void Configure(EntityTypeBuilder<ProductGroupPage> builder)
    {
        // Map FK to existing column
        builder.Property(x => x.ProductGroupId).HasColumnName(nameof(ProductGroupPage.ProductGroupId));
        
        //Indexes
        builder.HasIndex(x => new { x.ProductGroupId, x.DeletedAt }).IsUnique()
            .HasDatabaseName(
                $"UK_{nameof(ProductGroupPage)}_{nameof(ProductGroupPage.ProductGroupId)}_{nameof(ProductGroupPage.DeletedAt)}");
        
        //Relations.
        builder.HasOne(x => x.ProductGroup)
            .WithOne(x => x.Page)
            .HasForeignKey<ProductGroupPage>(x => x.ProductGroupId)
            .HasPrincipalKey<ProductGroup>(p => p.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}