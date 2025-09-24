using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class DiscountProductConfiguration : IEntityTypeConfiguration<DiscountProduct>
{
    public void Configure(EntityTypeBuilder<DiscountProduct> builder)
    {
        //Table.
        builder.ToTable(nameof(DiscountProduct));

        //PK
        builder.HasKey(x => new { x.DiscountId, x.ProductId, x.DeletedAt });
        
        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);
        
        //Properties.
        builder.Property(x => x.DiscountId).HasColumnType("integer").HasColumnOrder(1);
        builder.Property(x => x.ProductId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.CreatedBy).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(52);
        builder.Property(x => x.DeletedBy).HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(58);
        
        //Relations.
        builder.HasOne(x => x.Discount)
            .WithMany()
            .HasForeignKey(x => x.DiscountId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => (AppUser?)x.CreatedByUser)
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x =>  (AppUser?)x.DeletedByUser)
            .WithMany()
            .HasForeignKey(x => x.DeletedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}