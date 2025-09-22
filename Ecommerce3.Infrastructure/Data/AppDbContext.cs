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

        builder.ApplyConfiguration(new BrandConfiguration());

        // Rename Identity tables
        builder.Entity<Role>().ToTable("Role");
        builder.Entity<IdentityUserRole<int>>().ToTable("AppUserRole");
        builder.Entity<IdentityUserClaim<int>>().ToTable("AppUserClaim");
        builder.Entity<IdentityUserLogin<int>>().ToTable("AppUserLogin");
        builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim");
        builder.Entity<IdentityUserToken<int>>().ToTable("AppUserToken");
    }
}