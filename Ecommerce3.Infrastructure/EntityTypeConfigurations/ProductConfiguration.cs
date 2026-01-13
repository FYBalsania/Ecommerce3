using System.Net.NetworkInformation;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        //Table
        builder.ToTable(nameof(Product));

        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Filters
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Navigation Properties.
        builder.Navigation(x => x.Images).HasField("_images").UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(x => x.Categories).HasField("_categories").UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(x => x.TextListItems).HasField("_textListItems")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(x => x.KVPListItems).HasField("_kvpListItems")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(x => x.QnAs).HasField("_qnas").UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(x => x.Reviews).HasField("_reviews").UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(x => x.Attributes).HasField("_attributes").UsePropertyAccessMode(PropertyAccessMode.Field);

        //Properties
        builder.Property(x => x.SKU).HasMaxLength(Product.SKUMaxLength)
            .HasColumnType($"varchar({Product.SKUMaxLength})").HasColumnOrder(2);
        builder.Property(x => x.GTIN).HasMaxLength(Product.GTINMaxLength)
            .HasColumnType($"varchar({Product.GTINMaxLength})").HasColumnOrder(3);
        builder.Property(x => x.MPN).HasMaxLength(Product.MPNMaxLength)
            .HasColumnType($"varchar({Product.MPNMaxLength})").HasColumnOrder(4);
        builder.Property(x => x.MFC).HasMaxLength(Product.MFCMaxLength)
            .HasColumnType($"varchar({Product.MFCMaxLength})").HasColumnOrder(5);
        builder.Property(x => x.EAN).HasMaxLength(Product.EANMaxLength)
            .HasColumnType($"varchar({Product.EANMaxLength})").HasColumnOrder(6);
        builder.Property(x => x.UPC).HasMaxLength(Product.UPCMaxLength)
            .HasColumnType($"varchar({Product.UPCMaxLength})").HasColumnOrder(7);
        builder.Property(x => x.Name).HasMaxLength(Product.NameMaxLength).HasColumnType("citext").HasColumnOrder(8);
        builder.Property(x => x.Slug).HasMaxLength(Product.SlugMaxLength).HasColumnType("citext").HasColumnOrder(9);
        builder.Property(x => x.Display).HasMaxLength(Product.DisplayMaxLength).HasColumnType("citext")
            .HasColumnOrder(10);
        builder.Property(x => x.Breadcrumb).HasMaxLength(Product.BreadcrumbMaxLength).HasColumnType("citext")
            .HasColumnOrder(11);
        builder.Property(x => x.AnchorText).HasMaxLength(Product.AnchorTextMaxLength).HasColumnType("citext")
            .HasColumnOrder(12);
        builder.Property(x => x.AnchorTitle).HasMaxLength(Product.AnchorTitleMaxLength).HasColumnType("citext")
            .HasColumnOrder(13);
        builder.Property(x => x.BrandId).HasColumnType("integer").HasColumnOrder(14);
        builder.Property(x => x.ProductGroupId).HasColumnType("integer").HasColumnOrder(15);
        builder.Property(x => x.ShortDescription).HasMaxLength(Product.ShortDescriptionMaxLength)
            .HasColumnType("varchar(512)").HasColumnOrder(16);
        builder.Property(x => x.FullDescription).HasColumnType("text").HasColumnOrder(17);
        builder.Property(x => x.AllowReviews).HasColumnType("boolean").HasColumnOrder(18);
        builder.Property(x => x.AverageRating).HasColumnType("decimal(18,2)").HasColumnOrder(19);
        builder.Property(x => x.TotalReviews).HasColumnType("integer").HasColumnOrder(20);
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)").HasColumnOrder(21);
        builder.Property(x => x.OldPrice).HasColumnType("decimal(18,2)").HasColumnOrder(22);
        builder.Property(x => x.CostPrice).HasColumnType("decimal(18,2)").HasColumnOrder(23);
        builder.Property(x => x.Stock).HasColumnType("decimal(18,2)").HasColumnOrder(24);
        builder.Property(x => x.MinStock).HasColumnType("decimal(18,2)").HasColumnOrder(25);
        builder.Property(x => x.ShowAvailability).HasColumnType("boolean").HasColumnOrder(26);
        builder.Property(x => x.FreeShipping).HasColumnType("boolean").HasColumnOrder(27);
        builder.Property(x => x.AdditionalShippingCharge).HasColumnType("decimal(18,2)").HasColumnOrder(28);
        builder.Property(x => x.UnitOfMeasureId).HasColumnType("integer").HasColumnOrder(29);
        builder.Property(x => x.QuantityPerUnitOfMeasure).HasColumnType("decimal(18,3)").HasColumnOrder(30);
        builder.Property(x => x.DeliveryWindowId).HasColumnType("integer").HasColumnOrder(31);
        builder.Property(x => x.MinOrderQuantity).HasColumnType("decimal(18,3)").HasColumnOrder(32);
        builder.Property(x => x.MaxOrderQuantity).HasColumnType("decimal(18,3)").HasColumnOrder(33);
        builder.Property(x => x.IsFeatured).HasColumnType("boolean").HasColumnOrder(34);
        builder.Property(x => x.IsNew).HasColumnType("boolean").HasColumnOrder(35);
        builder.Property(x => x.IsBestSeller).HasColumnType("boolean").HasColumnOrder(36);
        builder.Property(x => x.IsReturnable).HasColumnType("boolean").HasColumnOrder(37);
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(32).HasColumnType("varchar(32)")
            .HasColumnOrder(38);
        builder.Property(x => x.RedirectUrl).HasMaxLength(Product.RedirectUrlMaxLength).HasColumnType("citext")
            .HasColumnOrder(39);
        builder.Property(x => x.SortOrder).HasColumnType("decimal(18,3)").HasColumnOrder(40);
        builder.Property(x => x.CountryOfOriginId).HasColumnType("integer").HasColumnOrder(41);
        builder.Property(p => p.Facets).HasColumnName("facets").HasColumnType("text[]").HasDefaultValueSql("'{}'")
            .IsRequired().HasColumnOrder(42);
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasColumnName("created_by_ip").HasColumnType("inet").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasColumnName("updated_by_ip").HasColumnType("inet").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnName("deleted_by").HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnName("deleted_at").HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasColumnName("deleted_by_ip").HasColumnType("inet").HasColumnOrder(58);

        //Indexes
        builder.HasIndex(x => x.SKU).IsUnique().HasDatabaseName($"UK_{nameof(Product)}_{nameof(Product.SKU)}");
        builder.HasIndex(x => x.GTIN).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.GTIN)}");
        builder.HasIndex(x => x.MPN).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.MPN)}");
        builder.HasIndex(x => x.MFC).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.MFC)}");
        builder.HasIndex(x => x.EAN).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.EAN)}");
        builder.HasIndex(x => x.UPC).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.UPC)}");
        builder.HasIndex(x => x.Name).IsUnique()
            .HasDatabaseName($"UK_{nameof(Product)}_{nameof(Product.Name)}");
        builder.HasIndex(x => x.Slug).IsUnique()
            .HasDatabaseName($"UK_{nameof(Product)}_{nameof(Product.Slug)}");
        builder.HasIndex(x => x.Display).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.Display)}");
        builder.HasIndex(x => x.Breadcrumb).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.Breadcrumb)}");
        builder.HasIndex(x => x.AnchorText).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.AnchorText)}");
        builder.HasIndex(x => x.AnchorTitle).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.AnchorTitle)}");
        builder.HasIndex(x => x.RedirectUrl).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.RedirectUrl)}");
        builder.HasIndex(x => x.Status).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.Status)}");
        builder.HasIndex(x => x.Price).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.Price)}");
        // builder.HasIndex(x => x.IsFeatured).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.IsFeatured)}");
        // builder.HasIndex(x => x.IsNew).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.IsNew)}");
        // builder.HasIndex(x => x.IsBestSeller).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.IsBestSeller)}");
        builder.HasIndex(x => x.UnitOfMeasureId)
            .HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.UnitOfMeasureId)}");
        builder.HasIndex(x => x.QuantityPerUnitOfMeasure)
            .HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.QuantityPerUnitOfMeasure)}");
        builder.HasIndex(x => x.Stock).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.Stock)}");
        builder.HasIndex(x => x.CountryOfOriginId)
            .HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.CountryOfOriginId)}");
        builder.HasIndex(x => x.SortOrder).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.SortOrder)}");
        builder.HasIndex(x => x.Facets).HasMethod("gin");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"idx_product_created_at");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"idx_product_deleted_at");

        //Relations.
        builder.HasMany(x => x.Images)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Brand)
            .WithMany()
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ProductGroup)
            .WithMany()
            .HasForeignKey(x => x.ProductGroupId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.DeliveryWindow)
            .WithMany()
            .HasForeignKey(x => x.DeliveryWindowId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.UnitOfMeasure)
            .WithMany()
            .HasForeignKey(x => x.UnitOfMeasureId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.Categories)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.TextListItems)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.KVPListItems)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.QnAs)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.Reviews)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.Attributes)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.CountryOfOrigin)
            .WithMany()
            .HasForeignKey(x => x.CountryOfOriginId)
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