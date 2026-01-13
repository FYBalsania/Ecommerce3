using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductAttributeValueConfiguration : IEntityTypeConfiguration<ProductAttributeValue>
{
    public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
    {
        //Table.
        builder.ToTable(nameof(ProductAttributeValue));

        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);
        
        //Discriminator.
        builder.HasDiscriminator<string>("Discriminator")
            .HasValue<ProductAttributeValue>(nameof(ProductAttributeValue))
            .HasValue<ProductAttributeColourValue>(nameof(ProductAttributeColourValue))
            .HasValue<ProductAttributeBooleanValue>(nameof(ProductAttributeBooleanValue))
            .HasValue<ProductAttributeDateOnlyValue>(nameof(ProductAttributeDateOnlyValue))
            .HasValue<ProductAttributeDecimalValue>(nameof(ProductAttributeDecimalValue));

        //Properties.
        builder.Property(x => x.ProductAttributeId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property("Discriminator").HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(3);
        builder.Property(x => x.Value).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(4);
        builder.Property(x => x.Slug).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(5);
        builder.Property(x => x.Display).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(6);
        builder.Property(x => x.Breadcrumb).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(7);
        builder.Property(x => x.SortOrder).HasColumnType("decimal(18,2)").HasColumnOrder(14);
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasColumnName("created_by_ip").HasColumnType("inet").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasColumnName("updated_by_ip").HasColumnType("inet").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnName("deleted_by").HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnName("deleted_at").HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasColumnName("deleted_by_ip").HasColumnType("inet").HasColumnOrder(58);

        //Indexes.
        builder.HasIndex(x => new { x.ProductAttributeId, x.Value }).IsUnique()
            .HasDatabaseName(
                $"UK_{nameof(ProductAttributeValue)}_{nameof(ProductAttributeValue.ProductAttributeId)}_{nameof(ProductAttributeValue.Value)}");
        
        builder.HasIndex(x => new { x.ProductAttributeId, x.Slug }).IsUnique()
            .HasDatabaseName(
                $"UK_{nameof(ProductAttributeValue)}_{nameof(ProductAttributeValue.ProductAttributeId)}_{nameof(ProductAttributeValue.Slug)}");

        builder.HasIndex(x => x.Display).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(ProductAttributeValue)}_{nameof(ProductAttributeValue.Display)}");
        
        builder.HasIndex(x => x.Breadcrumb).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(ProductAttributeValue)}_{nameof(ProductAttributeValue.Breadcrumb)}");
        
        builder.HasIndex(x => new { x.ProductAttributeId, x.SortOrder, x.Value })
            .HasDatabaseName(
                $"IX_{nameof(ProductAttributeValue)}_{nameof(ProductAttributeValue.ProductAttributeId)}_{nameof(ProductAttributeValue.SortOrder)}_{nameof(ProductAttributeValue.Value)}");

        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"idx_product_attribute_value_created_at");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"idx_product_attribute_value_deleted_at");
        
        //Relations.
        // builder.HasOne(x => x.ProductAttribute)
        //     .WithMany(x => x.Values)
        //     .HasForeignKey(x => x.ProductAttributeId)
        //     .OnDelete(DeleteBehavior.Restrict);
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