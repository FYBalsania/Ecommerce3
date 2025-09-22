using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductAttributeColourValueConfiguration : IEntityTypeConfiguration<ProductAttributeColourValue>
{
    public void Configure(EntityTypeBuilder<ProductAttributeColourValue> builder)
    {
        //properties.
        builder.Property(x => x.HexCode).HasMaxLength(8).HasColumnType("varchar(8)").HasColumnOrder(12);
        builder.Property(x => x.ColourFamily).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(13);
        builder.Property(x => x.ColourFamilyHexCode).HasMaxLength(8).HasColumnType("varchar(8)").HasColumnOrder(14);
    }
}