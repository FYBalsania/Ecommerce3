using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        //Table.
        builder.ToTable(nameof(Cart));

        //Key.
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Properties.
        builder.Property(x => x.CustomerId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.SessionId).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(3);
        builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)").HasColumnOrder(4);
        builder.Property(x => x.Discount).HasColumnType("decimal(18,2)").HasColumnOrder(5);
        builder.Property(x => x.Total).HasColumnType("decimal(18,2)").HasColumnOrder(6);
        builder.Property(x => x.SalesOrderId).HasColumnType("integer").HasColumnOrder(7);
        builder.Property(x => x.CreatedBy).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(55);

        //Navigation
        builder.Navigation(x => x.Lines).HasField("_lines").UsePropertyAccessMode(PropertyAccessMode.Field);

        //Indexes.
        builder.HasIndex(x => x.SessionId).HasDatabaseName($"IX_{nameof(Cart)}_{nameof(Cart.SessionId)}");
        builder.HasIndex(x => x.Total).HasDatabaseName($"IX_{nameof(Cart)}_{nameof(Cart.Total)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(Cart)}_{nameof(Cart.CreatedAt)}");
        
        //Relations.
        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<SalesOrder>()
            .WithMany()
            .HasForeignKey(x => x.SalesOrderId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}