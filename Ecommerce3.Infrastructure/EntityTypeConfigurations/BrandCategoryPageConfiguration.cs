using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class BrandCategoryPageConfiguration : IEntityTypeConfiguration<BrandCategoryPage>
{
    public void Configure(EntityTypeBuilder<BrandCategoryPage> builder)
    {
        // Map FKs to existing columns
        builder.Property(x => x.BrandId).HasColumnName(nameof(BrandCategoryPage.BrandId));
        builder.Property(x => x.CategoryId).HasColumnName(nameof(BrandCategoryPage.CategoryId));
        
        // Relations
        builder.HasOne(x => x.Brand)
            .WithOne()
            .HasForeignKey<BrandCategoryPage>(x => x.BrandId)
            .HasPrincipalKey<Brand>(b => b.Id)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Category)
            .WithOne()
            .HasForeignKey<BrandCategoryPage>(x => x.CategoryId)
            .HasPrincipalKey<Category>(c => c.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}