using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Ecommerce3.Infrastructure.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Data;

internal class AppDbContext : IdentityDbContext<AppUser, Role, int>
{
    public DbSet<Brand> Brands { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Always call base first to apply Identity's default config.
        base.OnModelCreating(builder);

        // Apply entity configurations.
        builder.ApplyConfiguration(new BrandCategoryPageConfiguration());
        builder.ApplyConfiguration(new BrandConfiguration());
        builder.ApplyConfiguration(new BrandPageConfiguration());
        builder.ApplyConfiguration(new CartConfiguration());
        builder.ApplyConfiguration(new CartLineConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
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
        builder.ApplyConfiguration(new ProductAttributeColourValueConfiguration());
        builder.ApplyConfiguration(new ProductAttributeConfiguration());
        builder.ApplyConfiguration(new ProductAttributeValueConfiguration());
        builder.ApplyConfiguration(new ProductCategoryConfiguration());
        builder.ApplyConfiguration(new ProductConfiguration());
        builder.ApplyConfiguration(new ProductDiscountConfiguration());
        builder.ApplyConfiguration(new ProductGroupConfiguration());
        builder.ApplyConfiguration(new ProductGroupPageConfiguration());
        builder.ApplyConfiguration(new ProductGroupProductAttributeConfiguration());
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
        builder.Entity<Role>().ToTable("Role");
        builder.Entity<IdentityUserRole<int>>().ToTable("AppUserRole");
        builder.Entity<IdentityUserClaim<int>>().ToTable("AppUserClaim");
        builder.Entity<IdentityUserLogin<int>>().ToTable("AppUserLogin");
        builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
        builder.Entity<IdentityUserToken<int>>().ToTable("AppUserToken");
    }
}