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
        
        //Filters
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Properties.
        builder.Property(x => x.CustomerId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.SessionId).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(3);
        builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)").HasColumnOrder(4);
        builder.Property(x => x.Discount).HasColumnType("decimal(18,2)").HasColumnOrder(5);
        builder.Property(x => x.Total).HasColumnType("decimal(18,2)").HasColumnOrder(6);
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
        builder.HasIndex(x => x.SessionId).HasDatabaseName($"IX_{nameof(Cart)}_{nameof(Cart.SessionId)}");
        builder.HasIndex(x => x.Total).HasDatabaseName($"IX_{nameof(Cart)}_{nameof(Cart.Total)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(Cart)}_{nameof(Cart.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(Cart)}_{nameof(Cart.DeletedAt)}");
        
        //Relations.
        builder.HasMany(x => x.Lines)
            .WithOne(x => x.Cart)
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
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