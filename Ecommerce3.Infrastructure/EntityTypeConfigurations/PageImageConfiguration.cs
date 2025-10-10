using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public sealed class PageImageConfiguration : IEntityTypeConfiguration<PageImage>
{
    public void Configure(EntityTypeBuilder<PageImage> builder)
    {
        //Properties.
        builder.Property(x => x.PageId).HasColumnType("integer").HasColumnOrder(17);
        
        //Relations.
        builder.HasOne(x => x.Page)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.PageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}