using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductTextListItemConfiguration : IEntityTypeConfiguration<ProductTextListItem>
{
    public void Configure(EntityTypeBuilder<ProductTextListItem> builder)
    {
        //properties.
        builder.Property(x => x.ProductId).HasColumnType("integer").HasColumnOrder(6);

        //indexes.
        builder.HasIndex(x => new { x.ProductId,  x.Type, x.SortOrder })
            .HasDatabaseName(
                $"IX_{nameof(ProductTextListItem.ProductId)}_{nameof(TextListItem.Type)}_{nameof(TextListItem.SortOrder)}");

        //relations.
        builder.HasOne(x => x.Product)
            .WithMany(x => x.TextListItems)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}