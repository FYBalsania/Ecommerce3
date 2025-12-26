using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductProductAttributeConfiguration : IEntityTypeConfiguration<ProductProductAttribute>
{
    public void Configure(EntityTypeBuilder<ProductProductAttribute> builder)
    {
        //Table.
        builder.ToTable(nameof(ProductProductAttribute));

        //Key.
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Properties.
        builder.Property(x => x.ProductId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.ProductAttributeId).HasColumnType("integer").HasColumnOrder(3);
        builder.Property(x => x.ProductAttributeSortOrder).HasColumnType("decimal(18,2)").HasColumnOrder(4);
        builder.Property(x => x.ProductAttributeValueId).HasColumnType("integer").HasColumnOrder(5);
        builder.Property(x => x.ProductAttributeValueSortOrder).HasColumnType("decimal(18,2)").HasColumnOrder(6);
        builder.Property(x => x.CreatedBy).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(58);

        //Indexes.
        builder.HasIndex(x => new { x.ProductId, x.ProductAttributeId, x.ProductAttributeValueId, x.DeletedAt })
            .IsUnique()
            .HasDatabaseName(
                $"UK_{nameof(ProductProductAttribute)}_{nameof(ProductProductAttribute.ProductId)}_{nameof(ProductProductAttribute.ProductAttributeId)}_{nameof(ProductProductAttribute.ProductAttributeValueId)}_{nameof(ProductProductAttribute.DeletedAt)}");
        builder.HasIndex(x => x.DeletedAt)
            .HasDatabaseName($"IX_{nameof(ProductProductAttribute)}_{nameof(ProductProductAttribute.DeletedAt)}");

        //Relations.
        builder.HasOne(x => x.Product)
            .WithMany(x => x.Attributes)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.ProductAttribute)
            .WithMany()
            .HasForeignKey(x => x.ProductAttributeId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.ProductAttributeValue)
            .WithMany()
            .HasForeignKey(x => x.ProductAttributeValueId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => (AppUser?)x.CreatedByUser)
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => (AppUser?)x.UpdatedByUser)
            .WithMany()
            .HasForeignKey(x => x.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => (AppUser?)x.DeletedByUser)
            .WithMany()
            .HasForeignKey(x => x.DeletedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}