using Ecommerce3.Domain.Entities;
using Ecommerce3.Domain.Models;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class SalesOrderLineConfiguration : IEntityTypeConfiguration<SalesOrderLine>
{
    public void Configure(EntityTypeBuilder<SalesOrderLine> builder)
    {
        //Table.
        builder.ToTable(nameof(SalesOrderLine));

        //Key.
        builder.HasKey(s => s.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Properties.
        builder.Property(x => x.SalesOrderId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.CartLineId).HasColumnType("integer").HasColumnOrder(3);
        builder.Property(x => x.ProductId).HasColumnType("integer").HasColumnOrder(4);
        builder.Property<ProductReference>(x => x.ProductReference).HasColumnType("jsonb").HasColumnOrder(5);
        builder.Property(x => x.Quantity).HasColumnType("integer").HasColumnOrder(6);
        builder.Property(x => x.Price).HasColumnType("decimal(18,2)").HasColumnOrder(7);
        builder.Property(x => x.Total).HasColumnType("decimal(18,2)").HasColumnOrder(8);
        builder.Property(x => x.CreatedByUserId).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedByCustomerId).HasColumnType("integer").HasColumnOrder(51);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(52);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(53);
        builder.Property(x => x.UpdatedByUserId).HasColumnType("integer").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByCustomerId).HasColumnType("integer").HasColumnOrder(55);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(56);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(57);
        builder.Property(x => x.DeletedBy).HasColumnType("integer").HasColumnOrder(58);
        builder.Property(x => x.DeletedAt).HasColumnType("timestamp").HasColumnOrder(59);
        builder.Property(x => x.DeletedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(60);

        //Indexes
        builder.HasIndex(x => x.ProductReference).HasMethod("gin")
            .HasDatabaseName($"GIN_{nameof(SalesOrderLine)}_{nameof(SalesOrderLine.ProductReference)}");
        builder.HasIndex(x => x.Total).HasDatabaseName($"IX_{nameof(SalesOrderLine)}_{nameof(SalesOrderLine.Total)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(SalesOrderLine)}_{nameof(SalesOrderLine.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(SalesOrderLine)}_{nameof(SalesOrderLine.DeletedAt)}");

        //Relations.
        builder.HasOne(x => x.SalesOrder)
            .WithMany(x => x.Lines)
            .HasForeignKey(x => x.SalesOrderId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.CartLine)
            .WithMany()
            .HasForeignKey(x => x.CartLineId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => (AppUser?)x.CreatedByUser)
            .WithMany()
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.CreatedByCustomer)
            .WithMany()
            .HasForeignKey(x => x.CreatedByCustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => (AppUser?)x.UpdatedByUser)
            .WithMany()
            .HasForeignKey(x => x.UpdatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.UpdatedByCustomer)
            .WithMany()
            .HasForeignKey(x => x.UpdatedByCustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => (AppUser?)x.DeletedByUser)
            .WithMany()
            .HasForeignKey(x => x.DeletedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}