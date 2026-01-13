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
        builder.HasDiscriminator<string>("Discriminator")
            .HasValue<ShippingDiscount>(nameof(ShippingDiscount))
            .HasValue<ProductDiscount>(nameof(ProductDiscount));

        //Filters
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Properties.
        builder.Property("Discriminator").HasMaxLength(32).HasColumnType("varchar(32)").HasColumnOrder(2);
        builder.Property(x => x.Code).HasMaxLength(16).HasColumnType("citext").HasColumnOrder(3);
        builder.Property(x => x.Name).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(4);
        builder.Property(x => x.StartAt).HasColumnType("timestamp").HasColumnOrder(5);
        builder.Property(x => x.EndAt).HasColumnType("timestamp").HasColumnOrder(6);
        builder.Property(x => x.MinOrderValue).HasColumnType("decimal(18,2)").HasColumnOrder(7);
        builder.Property(x => x.Type).HasConversion<string>().HasMaxLength(16).HasColumnType("varchar(16)")
            .HasColumnOrder(8);
        builder.Property(x => x.Percent).HasColumnType("decimal(18,2)").HasColumnOrder(9);
        builder.Property(x => x.MaxDiscountAmount).HasColumnType("decimal(18,2)").HasColumnOrder(10);
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)").HasColumnOrder(11);
        builder.Property(x => x.IsActive).HasColumnType("boolean").HasColumnOrder(12);
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasColumnName("created_by_ip").HasColumnType("inet").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasColumnName("updated_by_ip").HasColumnType("inet").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnName("deleted_by").HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnName("deleted_at").HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasColumnName("deleted_by_ip").HasColumnType("inet").HasColumnOrder(58);

        //Indexes.
        builder.HasIndex("Discriminator").HasDatabaseName($"IX_{nameof(Discount)}_Discriminator");
        builder.HasIndex(x => x.Code).IsUnique()
            .HasDatabaseName($"UK_{nameof(Discount)}_{nameof(Discount.Code)}");
        builder.HasIndex(x => x.Name).IsUnique()
            .HasDatabaseName($"UK_{nameof(Discount)}_{nameof(Discount.Name)}");
        builder.HasIndex(x => x.StartAt)
            .HasDatabaseName($"IX_{nameof(Discount)}_{nameof(Discount.StartAt)}");
        builder.HasIndex(x => x.EndAt)
            .HasDatabaseName($"IX_{nameof(Discount)}_{nameof(Discount.EndAt)}");
        builder.HasIndex(x => x.IsActive).HasDatabaseName($"IX_{nameof(Discount)}_{nameof(Discount.IsActive)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"idx_discount_created_at");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"idx_discount_deleted_at");

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