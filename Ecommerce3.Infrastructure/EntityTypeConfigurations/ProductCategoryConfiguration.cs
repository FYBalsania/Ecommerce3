using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        //Table.
        builder.ToTable(nameof(ProductCategory));

        //Key
        builder.HasKey(x => new { x.ProductId, x.CategoryId }).HasName(
            $"PK_{nameof(ProductCategory)}_{nameof(ProductCategory.ProductId)}_{nameof(ProductCategory.CategoryId)}");
        
        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Properties.
        builder.Property(x => x.ProductId).HasColumnType("integer").HasColumnOrder(1);
        builder.Property(x => x.CategoryId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.IsPrimary).HasColumnType("boolean").HasColumnOrder(3);
        builder.Property(x => x.SortOrder).HasColumnType("integer").HasColumnOrder(4);
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
        builder.HasIndex(x => x.IsPrimary)
            .HasDatabaseName($"IX_{nameof(ProductCategory)}_{nameof(ProductCategory.IsPrimary)}");
        builder.HasIndex(x => x.CreatedAt)
            .HasDatabaseName($"IX_{nameof(ProductCategory)}_{nameof(ProductCategory.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt)
            .HasDatabaseName($"IX_{nameof(ProductCategory)}_{nameof(ProductCategory.DeletedAt)}");

        //Relations
        builder.HasOne(x => x.Product)
            .WithMany(x => x.Categories)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Category)
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
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