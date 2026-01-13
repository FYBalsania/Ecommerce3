using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public sealed class ProductGroupProductAttributeConfiguration : IEntityTypeConfiguration<ProductGroupProductAttribute>
{
    public void Configure(EntityTypeBuilder<ProductGroupProductAttribute> builder)
    {
        //Table.
        builder.ToTable(nameof(ProductGroupProductAttribute));

        //Key.
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Properties.
        builder.Property(x => x.ProductGroupId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.ProductAttributeId).HasColumnType("integer").HasColumnOrder(3);
        builder.Property(x => x.ProductAttributeSortOrder).HasColumnType("decimal(18,2)").HasColumnOrder(4);
        builder.Property(x => x.ProductAttributeValueId).HasColumnType("integer").HasColumnOrder(5);
        builder.Property(x => x.ProductAttributeValueSortOrder).HasColumnType("decimal(18,2)").HasColumnOrder(6);
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasColumnName("created_by_ip").HasColumnType("inet").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasColumnName("updated_by_ip").HasColumnType("inet").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnName("deleted_by").HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnName("deleted_at").HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasColumnName("deleted_by_ip").HasColumnType("inet").HasColumnOrder(58);

        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Indexes.
        builder.HasIndex(x => new { x.ProductGroupId, x.ProductAttributeId, x.ProductAttributeValueId }).IsUnique()
            .HasDatabaseName(
                $"UK_{nameof(ProductGroupProductAttribute)}_{nameof(ProductGroupProductAttribute.ProductGroupId)}_{nameof(ProductGroupProductAttribute.ProductAttributeId)}_{nameof(ProductGroupProductAttribute.ProductAttributeValueId)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"idx_product_group_product_attribute_created_at");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"idx_product_group_product_attribute_deleted_at");

        //Relations.
        builder.HasOne(x => x.ProductGroup)
            .WithMany(x => x.Attributes)
            .HasForeignKey(x => x.ProductGroupId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ProductAttribute)
            .WithMany()
            .HasForeignKey(x => x.ProductAttributeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ProductAttributeValue)
            .WithMany()
            .HasForeignKey(x => x.ProductAttributeValueId)
            .OnDelete(DeleteBehavior.Restrict);
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