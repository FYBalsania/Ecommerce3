using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class CustomerAddressConfiguration : IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        //Table.
        builder.ToTable(nameof(CustomerAddress));

        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Navigation Properties.
        builder.Navigation(x => x.History).HasField("_history").UsePropertyAccessMode(PropertyAccessMode.Field);

        //Properties.
        builder.Property(x => x.CustomerId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.Type).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(3);
        builder.Property(x => x.FullName).HasMaxLength(128).HasColumnType("citext").HasColumnOrder(4);
        builder.Property(x => x.PhoneNumber).HasMaxLength(64).HasColumnType("citext").HasColumnOrder(5);
        builder.Property(x => x.CompanyName).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(6);
        builder.Property(x => x.AddressLine1).HasMaxLength(512).HasColumnType("citext").HasColumnOrder(7);
        builder.Property(x => x.AddressLine2).HasMaxLength(512).HasColumnType("citext").HasColumnOrder(8);
        builder.Property(x => x.City).HasMaxLength(64).HasColumnType("citext").HasColumnOrder(9);
        builder.Property(x => x.StateOrProvince).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(10);
        builder.Property(x => x.PostalCode).HasMaxLength(16).HasColumnType("citext").HasColumnOrder(11);
        builder.Property(x => x.Landmark).HasMaxLength(512).HasColumnType("citext").HasColumnOrder(12);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(52);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(55);
        builder.Property(x => x.DeletedAt).HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(58);

        //Owned collections.
        builder.OwnsMany(x => x.History, nb =>
        {
            nb.ToJson();
            nb.Property(x => x.Type).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(1);
            nb.Property(x => x.FullName).HasMaxLength(128).HasColumnType("citext").HasColumnOrder(2);
            nb.Property(x => x.PhoneNumber).HasMaxLength(64).HasColumnType("citext").HasColumnOrder(3);
            nb.Property(x => x.CompanyName).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(4);
            nb.Property(x => x.AddressLine1).HasMaxLength(512).HasColumnType("citext").HasColumnOrder(5);
            nb.Property(x => x.AddressLine2).HasMaxLength(512).HasColumnType("citext").HasColumnOrder(6);
            nb.Property(x => x.City).HasMaxLength(64).HasColumnType("citext").HasColumnOrder(7);
            nb.Property(x => x.StateOrProvince).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(8);
            nb.Property(x => x.PostalCode).HasMaxLength(16).HasColumnType("citext").HasColumnOrder(9);
            nb.Property(x => x.Landmark).HasMaxLength(512).HasColumnType("citext").HasColumnOrder(10);
            nb.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(11);
            nb.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(12);
        });

        //Indexes.
        builder.HasIndex(x => x.FullName).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.FullName)}");
        builder.HasIndex(x => x.PhoneNumber).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.PhoneNumber)}");
        builder.HasIndex(x => x.CompanyName).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.CompanyName)}");
        builder.HasIndex(x => x.AddressLine1).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.AddressLine1)}");
        builder.HasIndex(x => x.AddressLine2).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.AddressLine2)}");
        builder.HasIndex(x => x.City).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.City)}");
        builder.HasIndex(x => x.StateOrProvince).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.StateOrProvince)}");
        builder.HasIndex(x => x.PostalCode).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.PostalCode)}");
        builder.HasIndex(x => x.Landmark).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.Landmark)}");
        builder.HasIndex(x => x.CreatedAt)
            .HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt)
            .HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.DeletedAt)}");

        //Relations.
        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Addresses)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}