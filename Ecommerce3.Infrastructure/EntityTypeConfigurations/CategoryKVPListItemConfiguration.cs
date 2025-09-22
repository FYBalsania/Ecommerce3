using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class CategoryKVPListItemConfiguration : IEntityTypeConfiguration<CategoryKVPListItem>
{
    public void Configure(EntityTypeBuilder<CategoryKVPListItem> builder)
    {
        //Properties.
        builder.Property(x => x.CategoryId).HasColumnType("integer").HasColumnOrder(6);

        //Indexes.
        builder.HasIndex(x => new { x.CategoryId, x.Type })
            .HasDatabaseName(
                $"IX_{nameof(CategoryKVPListItem)}_{nameof(CategoryKVPListItem.CategoryId)}_{nameof(CategoryKVPListItem.Type)}");
        
        //Relations.
        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}