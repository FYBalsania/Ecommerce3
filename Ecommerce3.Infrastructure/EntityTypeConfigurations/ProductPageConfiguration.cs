using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductPageConfiguration : IEntityTypeConfiguration<ProductPage>
{
    public void Configure(EntityTypeBuilder<ProductPage> builder)
    {
        // Map FK to existing column
        builder.Property(x => x.ProductId).HasColumnName(nameof(ProductPage.ProductId));

        //Indexes.
        builder.HasIndex(x => new { x.ProductId, x.DeletedAt }).IsUnique()
            .HasDatabaseName($"UK_ProductPage_{nameof(ProductPage.ProductId)}_{nameof(ProductPage.DeletedAt)}");
        
        // One-to-one relation
        builder.HasOne(x => x.Product)
            .WithOne(x => x.Page)
            .HasForeignKey<ProductPage>(x => x.ProductId)
            .HasPrincipalKey<Product>(p => p.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}