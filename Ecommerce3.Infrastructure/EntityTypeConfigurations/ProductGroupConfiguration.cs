using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductGroupConfiguration : IEntityTypeConfiguration<ProductGroup>
{
    public void Configure(EntityTypeBuilder<ProductGroup> builder)
    {
        //Table.
        builder.ToTable(nameof(ProductGroup));

        //PK.
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Navigation.
        builder.Navigation(x => x.Attributes).HasField("_attributes").UsePropertyAccessMode(PropertyAccessMode.Field);

        //Properties.
        builder.Property(x => x.Name).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(2);
        builder.Property(x => x.Slug).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(3);
        builder.Property(x => x.Display).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(4);
        builder.Property(x => x.Breadcrumb).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(5);
        builder.Property(x => x.AnchorText).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(6);
        builder.Property(x => x.AnchorTitle).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(7);
        builder.Property(x => x.MetaTitle).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(8);
        builder.Property(x => x.MetaDescription).HasMaxLength(1024).HasColumnType("varchar(1024)").HasColumnOrder(9);
        builder.Property(x => x.MetaKeywords).HasMaxLength(1024).HasColumnType("varchar(1024)").HasColumnOrder(10);
        builder.Property(x => x.H1).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(11);
        builder.Property(x => x.ShortDescription).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(12);
        builder.Property(x => x.FullDescription).HasColumnType("text").HasColumnOrder(13);
        builder.Property(x => x.IsActive).HasColumnType("boolean").HasColumnOrder(14);
        builder.Property(x => x.SortOrder).HasColumnType("integer").HasColumnOrder(15);
        builder.Property(x => x.CreatedBy).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(58);
        
        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Indexes.
        builder.HasIndex(x => x.Name).IsUnique()
            .HasDatabaseName($"UK_{nameof(ProductGroup)}_{nameof(ProductGroup.Name)}");
        builder.HasIndex(x => x.Slug).IsUnique()
            .HasDatabaseName($"UK_{nameof(ProductGroup)}_{nameof(ProductGroup.Slug)}");
        builder.HasIndex(x => x.IsActive).IsDescending()
            .HasDatabaseName($"IX_{nameof(ProductGroup)}_{nameof(ProductGroup.IsActive)}");
        builder.HasIndex(x => x.SortOrder)
            .HasDatabaseName($"IX_{nameof(ProductGroup)}_{nameof(ProductGroup.SortOrder)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(ProductGroup)}_{nameof(ProductGroup.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(ProductGroup)}_{nameof(ProductGroup.DeletedAt)}");
        
        //Relations.
        builder.HasMany(x => x.Images)
            .WithOne()
            .HasForeignKey(x => x.ProductGroupId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.Attributes)
            .WithOne(x => x.ProductGroup)
            .HasForeignKey(x => x.ProductGroupId)
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