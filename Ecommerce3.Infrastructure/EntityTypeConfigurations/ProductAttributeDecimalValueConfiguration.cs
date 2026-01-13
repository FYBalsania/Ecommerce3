using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public sealed class ProductAttributeDecimalValueConfiguration : IEntityTypeConfiguration<ProductAttributeDecimalValue>
{
    public void Configure(EntityTypeBuilder<ProductAttributeDecimalValue> builder)
    {
        //properties.
        builder.Property(x => x.DecimalValue).HasColumnType("decimal(18,3)").HasColumnOrder(10);
        
        //indexes
        builder.HasIndex(x => new {x.ProductAttributeId, x.SortOrder, x.DecimalValue})
            .HasDatabaseName($"IX_{nameof(ProductAttributeDecimalValue)}_{nameof(ProductAttributeDecimalValue.ProductAttributeId)}_{nameof(ProductAttributeDecimalValue.SortOrder)}_{nameof(ProductAttributeDecimalValue.DecimalValue)}")
            .HasFilter("(\"deleted_at\") IS NULL");
    }
}