using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class SalesOrderConfiguration : IEntityTypeConfiguration<SalesOrder>
{
    public void Configure(EntityTypeBuilder<SalesOrder> builder)
    {
        //Table.
        builder.ToTable(nameof(SalesOrder));

        //Key.
        builder.HasKey(s => s.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);
        
        //Navigation Properties.
        builder.Navigation(x => x.Lines).HasField("_lines").UsePropertyAccessMode(PropertyAccessMode.Field);

        //Properties.
        builder.Property(x => x.Number).HasMaxLength(16).HasColumnType("varchar(16)").HasColumnOrder(2);
        builder.Property(x => x.Dated).HasColumnType("timestamp").HasColumnOrder(3);
        builder.Property(x => x.CartId).HasColumnType("integer").HasColumnOrder(4);
        builder.Property(x => x.CustomerId).HasColumnType("integer").HasColumnOrder(5);
        builder.Property(x => x.BillingCustomerAddressId).HasColumnType("integer").HasColumnOrder(7);
        builder.Property(x => x.ShippingCustomerAddressId).HasColumnType("integer").HasColumnOrder(9);
        builder.Property(x => x.SubTotal).HasColumnType("decimal(18,2)").HasColumnOrder(11);
        builder.Property(x => x.Discount).HasColumnType("decimal(18,2)").HasColumnOrder(12);
        builder.Property(x => x.ShippingCharge).HasColumnType("decimal(18,2)").HasColumnOrder(13);
        builder.Property(x => x.Total).HasColumnType("decimal(18,2)").HasColumnOrder(14);
        builder.Property(x => x.Status).HasConversion<string>().HasMaxLength(32).HasColumnType("varchar(32)")
            .HasColumnOrder(15);
        builder.Property(x => x.PaymentStatus).HasConversion<string>().HasMaxLength(32).HasColumnType("varchar(32)")
            .HasColumnOrder(16);
        builder.Property(x => x.ShippingStatus).HasConversion<string>().HasMaxLength(32).HasColumnType("varchar(32)")
            .HasColumnOrder(17);
        builder.Property(x => x.CreatedByUserId).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedByCustomerId).HasColumnType("integer").HasColumnOrder(51);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(52);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(53);
        builder.Property(x => x.UpdatedByUserId).HasColumnType("integer").HasColumnOrder(55);
        builder.Property(x => x.UpdatedByCustomerId).HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(57);
        builder.Property(x => x.DeletedBy).HasColumnType("integer").HasColumnOrder(58);
        builder.Property(x => x.DeletedAt).HasColumnType("timestamp").HasColumnOrder(59);
        builder.Property(x => x.DeletedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(60);

        //Owned Types.
        builder.OwnsOne(x => x.CustomerReference, nb =>
        {
            nb.ToJson();
            nb.Property(x => x.Id).HasColumnType("integer").HasColumnOrder(1);
            nb.Property(x => x.FirstName).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(2);
            nb.Property(x => x.LastName).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(3);
            nb.Property(x => x.CompanyName).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(4);
            nb.Property(x => x.PhoneNumber).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(5);
            nb.Property(x => x.IsEmailVerified).HasColumnType("boolean").HasColumnOrder(6);
        });

        builder.OwnsOne(x => x.BillingAddressReference, nb =>
        {
            nb.ToJson();
            nb.Property(x => x.Id).HasColumnType("integer").HasColumnOrder(1);
            nb.Property(x => x.Type).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(2);
            nb.Property(x => x.FullName).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(3);
            nb.Property(x => x.PhoneNumber).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(4);
            nb.Property(x => x.CompanyName).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(5);
            nb.Property(x => x.AddressLine1).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(6);
            nb.Property(x => x.AddressLine2).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(7);
            nb.Property(x => x.City).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(8);
            nb.Property(x => x.StateOrProvince).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(9);
            nb.Property(x => x.PostalCode).HasMaxLength(16).HasColumnType("varchar(16)").HasColumnOrder(10);
            nb.Property(x => x.Landmark).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(11);
        });
        
        builder.OwnsOne(x => x.ShippingAddressReference, nb =>
        {
            nb.ToJson();
            nb.Property(x => x.Id).HasColumnType("integer").HasColumnOrder(1);
            nb.Property(x => x.Type).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(2);
            nb.Property(x => x.FullName).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(3);
            nb.Property(x => x.PhoneNumber).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(4);
            nb.Property(x => x.CompanyName).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(5);
            nb.Property(x => x.AddressLine1).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(6);
            nb.Property(x => x.AddressLine2).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(7);
            nb.Property(x => x.City).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(8);
            nb.Property(x => x.StateOrProvince).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(9);
            nb.Property(x => x.PostalCode).HasMaxLength(16).HasColumnType("varchar(16)").HasColumnOrder(10);
            nb.Property(x => x.Landmark).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(11);
        });

        //Indexes
        builder.HasIndex(x => x.Number).IsUnique()
            .HasDatabaseName($"UK_{nameof(SalesOrder)}_{nameof(SalesOrder.Number)}");
        builder.HasIndex(x => x.Dated).IsDescending()
            .HasDatabaseName($"IX_{nameof(SalesOrder)}_{nameof(SalesOrder.Dated)}_D");
        builder.HasIndex(x => x.Total).HasDatabaseName($"{nameof(SalesOrder)}_{nameof(SalesOrder.Total)}");
        builder.HasIndex(x => x.Status).HasDatabaseName($"{nameof(SalesOrder)}_{nameof(SalesOrder.Status)}");
        builder.HasIndex(x => x.PaymentStatus)
            .HasDatabaseName($"{nameof(SalesOrder)}_{nameof(SalesOrder.PaymentStatus)}");
        builder.HasIndex(x => x.ShippingStatus)
            .HasDatabaseName($"{nameof(SalesOrder)}_{nameof(SalesOrder.ShippingStatus)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(SalesOrder)}_{nameof(SalesOrder.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(SalesOrder)}_{nameof(SalesOrder.DeletedAt)}");

        //Relations.
        builder.HasMany(x => x.Lines)
            .WithOne(x => x.SalesOrder)
            .HasForeignKey(x => x.SalesOrderId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Cart)
            .WithMany()
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.BillingAddress)
            .WithMany()
            .HasForeignKey(x => x.BillingCustomerAddressId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.ShippingAddress)
            .WithMany()
            .HasForeignKey(x => x.ShippingCustomerAddressId)
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