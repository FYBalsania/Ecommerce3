using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public sealed class ProductAttributeBooleanValueConfiguration : IEntityTypeConfiguration<ProductAttributeBooleanValue>
{
    public void Configure(EntityTypeBuilder<ProductAttributeBooleanValue> builder)
    {
        //properties.
        builder.Property(x => x.BooleanValue).HasColumnType("boolean").HasColumnOrder(8);

        //indexes.
        builder.HasIndex(x => new { x.ProductAttributeId, x.SortOrder, x.BooleanValue })
            .HasDatabaseName(
                $"IX_{nameof(ProductAttributeBooleanValue)}_{nameof(ProductAttributeBooleanValue.ProductAttributeId)}_{nameof(ProductAttributeBooleanValue.SortOrder)}_{nameof(ProductAttributeBooleanValue.BooleanValue)}")
            .HasFilter("(\"deleted_at\") IS NULL");
    }
}