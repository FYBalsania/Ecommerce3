using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class CartLineConfiguration : IEntityTypeConfiguration<CartLine>
{
    public void Configure(EntityTypeBuilder<CartLine> builder)
    {
        //Table.
        builder.ToTable(nameof(CartLine));

        //Key
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Filters
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Auto Include.
        builder.Navigation(x => x.Product).AutoInclude();

        //Properties
        builder.Property(x => x.CartId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.ProductId).HasColumnType("integer").HasColumnOrder(3);
        builder.Property(x => x.Quantity).HasColumnType("integer").HasColumnOrder(4);
        builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)").HasColumnOrder(5);
        builder.Property(x => x.Discount).HasColumnType("decimal(18,2)").HasColumnOrder(6);
        builder.Property(x => x.Total).HasColumnType("decimal(18,2)").HasColumnOrder(7);
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
        builder.HasIndex(x => x.Total).HasDatabaseName($"IX_{nameof(CartLine)}_{nameof(CartLine.Total)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(CartLine)}_{nameof(CartLine.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(CartLine)}_{nameof(CartLine.DeletedAt)}");

        //Relations.
        builder.HasOne(x => x.Cart)
            .WithMany(x => x.Lines)
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.ProductId)
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