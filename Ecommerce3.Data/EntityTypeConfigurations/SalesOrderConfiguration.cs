using Ecommerce3.Data.Entities;
using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Data.EntityTypeConfigurations;

public class SalesOrderConfiguration : IEntityTypeConfiguration<SalesOrder>
{
    public void Configure(EntityTypeBuilder<SalesOrder> builder)
    {
        //Table.
        builder.ToTable(nameof(SalesOrder));

        //Key.
        builder.HasKey(s => s.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Navigation.
        builder.Navigation(x => x.Lines).HasField("_lines").UsePropertyAccessMode(PropertyAccessMode.Field);

        //Properties.
        builder.Property(x => x.Number).HasMaxLength(16).HasColumnType("varchar(16)").HasColumnOrder(2);
        builder.Property(x => x.Dated).HasColumnType("timestamp").HasColumnOrder(3);
        builder.Property(x => x.CartId).HasColumnType("integer").HasColumnOrder(4);
        builder.Property(x => x.CustomerId).HasColumnType("integer").HasColumnOrder(5);
        builder.Property(x => x.CustomerReference).HasColumnType("jsonb").HasColumnOrder(6);
        builder.Property(x => x.BillingCustomerAddressId).HasColumnType("integer").HasColumnOrder(7);
        builder.Property(x => x.BillingAddressReference).HasColumnType("jsonb").HasColumnOrder(8);
        builder.Property(x => x.ShippingCustomerAddressId).HasColumnType("integer").HasColumnOrder(9);
        builder.Property(x => x.ShippingAddressReference).HasColumnType("jsonb").HasColumnOrder(10);
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
        builder.Property(x => x.CreatedBy).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(55);

        //Owned types.
        builder.OwnsOne(x => x.CustomerReference, nb =>
        {
            nb.ToJson();
            nb.Property(x => x.Id).HasColumnOrder(1);
            nb.Property(x => x.FirstName).HasColumnOrder(2);
            nb.Property(x => x.LastName).HasColumnOrder(3);
            nb.Property(x => x.CompanyName).HasColumnOrder(4);
            nb.Property(x => x.PhoneNumber).HasColumnOrder(5);
            nb.Property(x => x.IsEmailVerified).HasColumnOrder(6);
        });
        builder.OwnsOne(x => x.BillingAddressReference, nb =>
        {
            nb.ToJson();
            nb.Property(x => x.Id).HasColumnOrder(1);
            nb.Property(x => x.Type).HasColumnOrder(2);
            nb.Property(x => x.FullName).HasColumnOrder(3);
            nb.Property(x => x.PhoneNumber).HasColumnOrder(4);
            nb.Property(x => x.CompanyName).HasColumnOrder(5);
            nb.Property(x => x.AddressLine1).HasColumnOrder(6);
            nb.Property(x => x.AddressLine2).HasColumnOrder(7);
            nb.Property(x => x.City).HasColumnOrder(8);
            nb.Property(x => x.StateOrProvince).HasColumnOrder(9);
            nb.Property(x => x.PostalCode).HasColumnOrder(10);
            nb.Property(x => x.Landmark).HasColumnOrder(11);
        });
        builder.OwnsOne(x => x.ShippingAddressReference, nb =>
        {
            nb.ToJson();
            nb.Property(x => x.Id).HasColumnOrder(1);
            nb.Property(x => x.Type).HasColumnOrder(2);
            nb.Property(x => x.FullName).HasColumnOrder(3);
            nb.Property(x => x.PhoneNumber).HasColumnOrder(4);
            nb.Property(x => x.CompanyName).HasColumnOrder(5);
            nb.Property(x => x.AddressLine1).HasColumnOrder(6);
            nb.Property(x => x.AddressLine2).HasColumnOrder(7);
            nb.Property(x => x.City).HasColumnOrder(8);
            nb.Property(x => x.StateOrProvince).HasColumnOrder(9);
            nb.Property(x => x.PostalCode).HasColumnOrder(10);
            nb.Property(x => x.Landmark).HasColumnOrder(11);
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
        builder.HasIndex(x => x.CustomerReference).HasMethod("gin")
            .HasDatabaseName($"GIN_{nameof(SalesOrder)}_{nameof(SalesOrder.CustomerReference)}");
        builder.HasIndex(x => x.BillingAddressReference).HasMethod("gin")
            .HasDatabaseName($"GIN_{nameof(SalesOrder)}_{nameof(SalesOrder.BillingAddressReference)}");
        builder.HasIndex(x => x.ShippingAddressReference).HasMethod("gin")
            .HasDatabaseName($"GIN_{nameof(SalesOrder)}_{nameof(SalesOrder.ShippingAddressReference)}");

        //Relations.
        builder.HasOne<Cart>()
            .WithMany()
            .HasForeignKey(x => x.CartId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<CustomerAddress>()
            .WithMany()
            .HasForeignKey(x => x.BillingCustomerAddressId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<CustomerAddress>()
            .WithMany()
            .HasForeignKey(x => x.ShippingCustomerAddressId)
            .OnDelete(DeleteBehavior.Restrict);
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