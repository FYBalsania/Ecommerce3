using System.Text.Json;
using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

        //JSONOptions.
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        // Value converter for List<string> <-> json string
        var converter = new ValueConverter<List<string>, string>(
            v => JsonSerializer.Serialize(v, jsonOptions),
            v => JsonSerializer.Deserialize<List<string>>(v, jsonOptions) ?? new()
        );
        
        // Value comparer so EF knows when list contents changed
        var comparer = new ValueComparer<List<string>>(
            (a, b) => a.SequenceEqual(b),
            v => v.Aggregate(0, (h, e) => HashCode.Combine(h, e != null ? e.GetHashCode() : 0)),
            v => v.ToList()
        );

        //Properties
        builder.Property(x => x.SKU).HasMaxLength(16).HasColumnType("varchar(16)").HasColumnOrder(2);
        builder.Property(x => x.GTIN).HasMaxLength(16).HasColumnType("varchar(16)").HasColumnOrder(3);
        builder.Property(x => x.MPN).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(4);
        builder.Property(x => x.MFC).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(5);
        builder.Property(x => x.EAN).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(6);
        builder.Property(x => x.UPC).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(7);
        builder.Property(x => x.Name).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(8);
        builder.Property(x => x.Slug).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(9);
        builder.Property(x => x.Display).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(10);
        builder.Property(x => x.Breadcrumb).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(11);
        builder.Property(x => x.AnchorText).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(12);
        builder.Property(x => x.AnchorTitle).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(13);
        builder.Property(x => x.MetaTitle).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(14);
        builder.Property(x => x.MetaDescription).HasMaxLength(1024).HasColumnType("varchar(1024)").HasColumnOrder(15);
        builder.Property(x => x.MetaKeywords).HasMaxLength(1024).HasColumnType("varchar(1024)").HasColumnOrder(16);
        builder.Property(x => x.H1).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(17);
        builder.Property(x => x.BrandId).HasColumnType("integer").HasColumnOrder(18);
        builder.Property(x => x.ProductGroupId).HasColumnType("integer").HasColumnOrder(19);
        builder.Property(x => x.ShortDescription).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(20);
        builder.Property(x => x.FullDescription).HasColumnType("text").HasColumnOrder(21);
        builder.Property(x => x.AllowReviews).HasColumnType("boolean").HasColumnOrder(22);
        builder.Property(x => x.AverageRating).HasColumnType("integer").HasColumnOrder(23);
        builder.Property(x => x.TotalReviews).HasColumnType("integer").HasColumnOrder(24);
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)").HasColumnOrder(25);
        builder.Property(x => x.OldPrice).HasColumnType("decimal(18,2)").HasColumnOrder(26);
        builder.Property(x => x.CostPrice).HasColumnType("decimal(18,2)").HasColumnOrder(27);
        builder.Property(x => x.Stock).HasColumnType("integer").HasColumnOrder(28);
        builder.Property(x => x.MinStock).HasColumnType("integer").HasColumnOrder(29);
        builder.Property(x => x.ShowAvailability).HasColumnType("boolean").HasColumnOrder(30);
        builder.Property(x => x.FreeShipping).HasColumnType("boolean").HasColumnOrder(31);
        builder.Property(x => x.AdditionalShippingCharge).HasColumnType("decimal(18,2)").HasColumnOrder(32);
        builder.Property(x => x.WeightKgs).HasColumnType("decimal(18,2)").HasColumnOrder(33);
        builder.Property(x => x.DeliveryWindowId).HasColumnType("integer").HasColumnOrder(34);
        builder.Property(x => x.MinOrderQuantity).HasColumnType("integer").HasColumnOrder(35);
        builder.Property(x => x.MaxOrderQuantity).HasColumnType("integer").HasColumnOrder(36);
        builder.Property(x => x.Featured).HasColumnType("boolean").HasColumnOrder(37);
        builder.Property(x => x.New).HasColumnType("boolean").HasColumnOrder(38);
        builder.Property(x => x.BestSeller).HasColumnType("boolean").HasColumnOrder(39);
        builder.Property(x => x.Returnable).HasColumnType("boolean").HasColumnOrder(40);
        builder.Property(x => x.ReturnPolicy).HasColumnType("text").HasColumnOrder(41);
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(32).HasColumnType("varchar(32)")
            .HasColumnOrder(42);
        builder.Property(x => x.RedirectUrl).HasMaxLength(2048).HasColumnType("varchar(2048)").HasColumnOrder(43);
        builder.Property(x => x.SortOrder).HasColumnType("integer").HasColumnOrder(44);
        builder.Property<List<string>>("_facets")
            .HasColumnName(nameof(Product.Facets))
            .HasColumnType("jsonb")
            .HasColumnOrder(45)
            .HasConversion(converter)
            .Metadata.SetValueComparer(comparer);
        builder.Property(x => x.CreatedBy).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(58);

        //Indexes
        builder.HasIndex("_facets").HasMethod("gin");
        builder.HasIndex(x => x.SKU).IsUnique().HasDatabaseName($"UK_{nameof(Product)}_{nameof(Product.SKU)}");
        builder.HasIndex(x => x.GTIN).IsUnique().HasDatabaseName($"UK_{nameof(Product)}_{nameof(Product.GTIN)}");
        builder.HasIndex(x => x.MPN).IsUnique().HasDatabaseName($"UK_{nameof(Product)}_{nameof(Product.MPN)}");
        builder.HasIndex(x => x.MFC).IsUnique().HasDatabaseName($"UK_{nameof(Product)}_{nameof(Product.MFC)}");
        builder.HasIndex(x => x.EAN).IsUnique().HasDatabaseName($"UK_{nameof(Product)}_{nameof(Product.EAN)}");
        builder.HasIndex(x => x.UPC).IsUnique().HasDatabaseName($"UK_{nameof(Product)}_{nameof(Product.UPC)}");
        builder.HasIndex(x => x.Name).IsUnique().HasDatabaseName($"UK_{nameof(Product)}_{nameof(Product.Name)}");
        builder.HasIndex(x => x.Slug).IsUnique().HasDatabaseName($"UK_{nameof(Product)}_{nameof(Product.Slug)}");
        builder.HasIndex(x => x.Status).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.Status)}");
        builder.HasIndex(x => x.SortOrder).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.SortOrder)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(Product)}_{nameof(Product.DeletedAt)}");

        //Navigations.

        //Relations.
        builder.HasMany(x => x.Images)
            .WithOne()
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
        builder.HasMany(x => x.Categories)
            .WithOne(x => x.Product)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
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