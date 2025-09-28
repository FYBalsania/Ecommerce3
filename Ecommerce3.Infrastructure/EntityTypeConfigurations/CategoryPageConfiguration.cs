using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class CategoryPageConfiguration : IEntityTypeConfiguration<CategoryPage>
{
    public void Configure(EntityTypeBuilder<CategoryPage> builder)
    {
        // Map FK to existing column
        builder.Property(x => x.CategoryId).HasColumnName(nameof(CategoryPage.CategoryId));
        
        //Relations.
        builder.HasOne(x => x.Category)
            .WithOne()
            .HasForeignKey<CategoryPage>(x => x.CategoryId)
            .HasPrincipalKey<Category>(x => x.Id)
            .OnDelete(DeleteBehavior.Restrict);
    }
}