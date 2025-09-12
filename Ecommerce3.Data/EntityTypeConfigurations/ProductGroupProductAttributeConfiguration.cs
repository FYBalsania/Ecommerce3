using Ecommerce3.Data.Entities;
using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Data.EntityTypeConfigurations;

public sealed class ProductGroupProductAttributeConfiguration : IEntityTypeConfiguration<ProductGroupProductAttribute>
{
    public void Configure(EntityTypeBuilder<ProductGroupProductAttribute> builder)
    {
        //Table.
        builder.ToTable(nameof(ProductGroupProductAttribute));

        //Key.
        builder.HasKey(x => new { x.ProductGroupId, x.ProductAttributeId, x.ProductAttributeValueId, x.DeletedAt });

        //Properties.
        builder.Property(x => x.ProductGroupId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.ProductAttributeId).HasColumnType("integer").HasColumnOrder(3);
        builder.Property(x => x.ProductAttributeSortOrder).HasColumnType("integer").HasColumnOrder(4);
        builder.Property(x => x.ProductAttributeValueId).HasColumnType("integer").HasColumnOrder(5);
        builder.Property(x => x.ProductAttributeValueSortOrder).HasColumnType("integer").HasColumnOrder(6);
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
        builder.HasIndex(x => x.CreatedAt)
            .HasDatabaseName(
                $"IX_{nameof(ProductGroupProductAttribute)}_{nameof(ProductGroupProductAttribute.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt)
            .HasDatabaseName(
                $"IX_{nameof(ProductGroupProductAttribute)}_{nameof(ProductGroupProductAttribute.DeletedAt)}");
        
        //Relations.
        builder.HasOne<ProductGroup>()
            .WithMany()
            .HasForeignKey(x => x.ProductGroupId);
        builder.HasOne<ProductAttribute>()
            .WithMany()
            .HasForeignKey(x => x.ProductAttributeId);
        builder.HasOne<ProductAttributeValue>()
            .WithMany()
            .HasForeignKey(x => x.ProductAttributeValueId);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.DeletedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}