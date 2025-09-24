using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductKVPListItemConfiguration : IEntityTypeConfiguration<ProductKVPListItem>
{
    public void Configure(EntityTypeBuilder<ProductKVPListItem> builder)
    {
        //Properties.
        builder.Property(x => x.ProductId).HasColumnType("integer").HasColumnOrder(8);

        //Indexes.
        builder.HasIndex(x => new { x.ProductId, x.Type })
            .HasDatabaseName(
                $"IX_{nameof(ProductKVPListItem)}_{nameof(ProductKVPListItem.ProductId)}_{nameof(ProductKVPListItem.Type)}");
        
        //Relations
        builder.HasOne(x => x.Product)
            .WithMany(x => x.KVPListItems)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}