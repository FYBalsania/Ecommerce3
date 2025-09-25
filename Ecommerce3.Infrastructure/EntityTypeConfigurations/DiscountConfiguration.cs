using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        //Table.
        builder.ToTable(nameof(Discount));

        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Discriminator.
        builder.HasDiscriminator<string>(x => x.Scope)
            .HasValue<Discount>(nameof(Discount))
            .HasValue<ShippingDiscount>(nameof(ShippingDiscount))
            .HasValue<ProductDiscount>(nameof(ProductDiscount));
        
        //Filters
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Properties.
        builder.Property(x => x.Scope).HasMaxLength(32).HasColumnType("varchar(32)").HasColumnOrder(2);
        builder.Property(x => x.Code).HasMaxLength(16).HasColumnType("varchar(16)").HasColumnOrder(3);
        builder.Property(x => x.Name).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(4);
        builder.Property(x => x.StartAt).HasColumnType("timestamp").HasColumnOrder(5);
        builder.Property(x => x.EndAt).HasColumnType("timestamp").HasColumnOrder(6);
        builder.Property(x => x.MinOrderValue).HasColumnType("decimal(18,2)").HasColumnOrder(7);
        builder.Property(x => x.Type).HasConversion<string>().HasMaxLength(16).HasColumnType("varchar(16)")
            .HasColumnOrder(8);
        builder.Property(x => x.Percent).HasColumnType("decimal(18,2)").HasColumnOrder(9);
        builder.Property(x => x.MaxDiscountAmount).HasColumnType("decimal(18,2)").HasColumnOrder(10);
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)").HasColumnOrder(11);
        builder.Property(x => x.IsActive).HasColumnType("boolean").HasColumnOrder(12);
        builder.Property(x => x.CreatedBy).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(58);

        //Indexes.
        builder.HasIndex(x => x.Scope).HasDatabaseName($"IX_{nameof(Discount)}_{nameof(Discount.Scope)}");
        builder.HasIndex(x => x.Code).IsUnique().HasDatabaseName($"UK_{nameof(Discount)}_{nameof(Discount.Code)}");
        builder.HasIndex(x => new { x.StartAt, x.EndAt })
            .HasDatabaseName($"IX_{nameof(Discount)}_{nameof(Discount.StartAt)}_{nameof(Discount.EndAt)}");
        builder.HasIndex(x => x.IsActive).HasDatabaseName($"IX_{nameof(Discount)}_{nameof(Discount.IsActive)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(Discount)}_{nameof(Discount.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(Discount)}_{nameof(Discount.DeletedAt)}");

        //Relations.
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