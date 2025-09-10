using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Data.EntityTypeConfigurations;

public class ProductTextListItemConfiguration : IEntityTypeConfiguration<ProductTextListItem>
{
    public void Configure(EntityTypeBuilder<ProductTextListItem> builder)
    {
        //properties.
        builder.Property(x => x.ProductId).HasColumnType("integer").HasColumnOrder(6);

        //indexes.
        builder.HasIndex(x => new { x.ProductId, x.TextListItemType, x.SortOrder })
            .HasDatabaseName(
                $"IX_{nameof(ProductTextListItem.ProductId)}_{nameof(TextListItem.TextListItemType)}_{nameof(TextListItem.SortOrder)}");

        //relations.
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}