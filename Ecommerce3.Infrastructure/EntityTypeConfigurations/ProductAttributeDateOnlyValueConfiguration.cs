using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public sealed class ProductAttributeDateOnlyValueConfiguration : IEntityTypeConfiguration<ProductAttributeDateOnlyValue>
{
    public void Configure(EntityTypeBuilder<ProductAttributeDateOnlyValue> builder)
    {
        //properties.
        builder.Property(x => x.DateOnlyValue).HasColumnType("date").HasColumnOrder(9);

        //indexes.
        builder.HasIndex(x => new { x.ProductAttributeId, x.SortOrder, x.DateOnlyValue })
            .HasDatabaseName(
                $"IX_{nameof(ProductAttributeDateOnlyValue)}_{nameof(ProductAttributeDateOnlyValue.ProductAttributeId)}_{nameof(ProductAttributeDateOnlyValue.SortOrder)}_{nameof(ProductAttributeDateOnlyValue.DateOnlyValue)}")
            .HasFilter("(\"DeletedAt\") IS NULL");
    }
}