using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Ecommerce3.Infrastructure.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Data;

internal class AppDbContext : IdentityDbContext<AppUser, Role, int>
{
    public DbSet<Bank> Banks { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<BrandCategoryPage> BrandCategoryPages { get; set; }
    public DbSet<BrandImage> BrandImages { get; set; }   
    public DbSet<BrandPage> BrandPages { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartLine> CartLines { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CategoryImage> CategoryImages { get; set; }   
    public DbSet<CategoryKVPListItem> CategoryKVPListItems { get; set; }
    public DbSet<CategoryPage> CategoryPages { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<CustomerAddress> CustomerAddresses { get; set; }
    public DbSet<DeliveryWindow> DeliveryWindows { get; set; }
    public DbSet<Discount> Discounts { get; set; }
    public DbSet<DiscountProduct> DiscountProducts { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<ImageType> ImageTypes { get; set; }
    public DbSet<KVPListItem> KVPListItems { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<PageImage> PageImages { get; set; }   
    public DbSet<PostCode> PostCodes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }
    public DbSet<ProductAttributeBooleanValue> ProductAttributeBooleanValues { get; set; }
    public DbSet<ProductAttributeColourValue> ProductAttributeColourValues { get; set; }
    public DbSet<ProductAttributeDateOnlyValue> ProductAttributeDateOnlyValues { get; set; }
    public DbSet<ProductAttributeDecimalValue> ProductAttributeDecimalValues { get; set; }
    public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductDiscount> ProductDiscounts { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<ProductGroupImage> ProductGroupImages { get; set; }  
    public DbSet<ProductGroupPage> ProductGroupPages { get; set; }
    public DbSet<ProductGroupProductAttribute> ProductGroupProductAttributes { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; } 
    public DbSet<ProductKVPListItem> ProductKVPListItems { get; set; }
    public DbSet<ProductPage> ProductPages { get; set; }
    public DbSet<ProductProductAttribute> ProductProductAttributes { get; set; }
    public DbSet<ProductQnA> ProductQnAs { get; set; }
    public DbSet<ProductReview> ProductReviews { get; set; }
    public DbSet<ProductTextListItem> ProductTextListItems { get; set; }
    public DbSet<SalesOrder> SalesOrders { get; set; }
    public DbSet<SalesOrderLine> SalesOrderLines { get; set; }
    public DbSet<ShippingDiscount> ShippingDiscounts { get; set; }
    public DbSet<TextListItem> TextListItems { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Always call base first to apply Identity's default config.
        base.OnModelCreating(builder);

        builder.HasPostgresExtension("citext");
        builder.HasPostgresExtension("pg_trgm");
        builder.HasPostgresExtension("ltree");

        // Apply entity configurations.
        builder.ApplyConfiguration(new BankConfiguration());
        builder.ApplyConfiguration(new BankImageConfiguration());
        builder.ApplyConfiguration(new BrandCategoryPageConfiguration());
        builder.ApplyConfiguration(new BrandConfiguration());
        builder.ApplyConfiguration(new BrandImageConfiguration());
        builder.ApplyConfiguration(new BrandPageConfiguration());
        builder.ApplyConfiguration(new CartConfiguration());
        builder.ApplyConfiguration(new CartLineConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new CategoryImageConfiguration());
        builder.ApplyConfiguration(new CategoryKVPListItemConfiguration());
        builder.ApplyConfiguration(new CategoryPageConfiguration());
        builder.ApplyConfiguration(new CustomerAddressConfiguration());
        builder.ApplyConfiguration(new CustomerConfiguration());
        builder.ApplyConfiguration(new DeliveryWindowConfiguration());
        builder.ApplyConfiguration(new DiscountConfiguration());
        builder.ApplyConfiguration(new DiscountProductConfiguration());
        builder.ApplyConfiguration(new ImageConfiguration());
        builder.ApplyConfiguration(new ImageTypeConfiguration());
        builder.ApplyConfiguration(new KVPListItemConfiguration());
        builder.ApplyConfiguration(new PageConfiguration());
        builder.ApplyConfiguration(new PageImageConfiguration());
        builder.ApplyConfiguration(new PostCodeConfiguration());
        builder.ApplyConfiguration(new ProductAttributeBooleanValueConfiguration());
        builder.ApplyConfiguration(new ProductAttributeColourValueConfiguration());
        builder.ApplyConfiguration(new ProductAttributeConfiguration());
        builder.ApplyConfiguration(new ProductAttributeDateOnlyValueConfiguration());
        builder.ApplyConfiguration(new ProductAttributeDecimalValueConfiguration());
        builder.ApplyConfiguration(new ProductAttributeValueConfiguration());
        builder.ApplyConfiguration(new ProductCategoryConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new ProductDiscountConfiguration());
        builder.ApplyConfiguration(new ProductGroupConfiguration());
        builder.ApplyConfiguration(new ProductGroupImageConfiguration());
        builder.ApplyConfiguration(new ProductGroupPageConfiguration());
        builder.ApplyConfiguration(new ProductGroupProductAttributeConfiguration());
        builder.ApplyConfiguration(new ProductImageConfiguration());
        builder.ApplyConfiguration(new ProductKVPListItemConfiguration());
        builder.ApplyConfiguration(new ProductPageConfiguration());
        builder.ApplyConfiguration(new ProductProductAttributeConfiguration());
        builder.ApplyConfiguration(new ProductQnAConfiguration());
        builder.ApplyConfiguration(new ProductReviewConfiguration());
        builder.ApplyConfiguration(new ProductTextListItemConfiguration());
        builder.ApplyConfiguration(new SalesOrderConfiguration());
        builder.ApplyConfiguration(new SalesOrderLineConfiguration());
        builder.ApplyConfiguration(new ShippingDiscountConfiguration());
        builder.ApplyConfiguration(new TextListItemConfiguration());

        // Rename Identity tables.
        builder.Entity<AppUser>().ToTable("AppUser");
        builder.Entity<Role>().ToTable("Role");
        builder.Entity<IdentityUserRole<int>>().ToTable("AppUserRole");
        builder.Entity<IdentityUserClaim<int>>().ToTable("AppUserClaim");
        builder.Entity<IdentityUserLogin<int>>().ToTable("AppUserLogin");
        builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
        builder.Entity<IdentityUserToken<int>>().ToTable("AppUserToken");
    }
}