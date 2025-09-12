using Ecommerce3.Data.Entities;
using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Data.EntityTypeConfigurations;

public class CustomerAddressConfiguration: IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(EntityTypeBuilder<CustomerAddress> builder)
    {
        //Table.
        builder.ToTable(nameof(CustomerAddress));
        
        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);
        
        //Properties.
        builder.Property(x => x.CustomerId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.Type).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(3);
        builder.Property(x => x.AddressLine1).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(4);
        builder.Property(x => x.AddressLine2).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(5);
        builder.Property(x => x.Landmark).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(6);
        builder.Property(x => x.PostalCode).HasMaxLength(16).HasColumnType("varchar(16)").HasColumnOrder(7);
        builder.Property(x => x.City).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(8);
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
        builder.HasIndex(x => x.PostalCode).HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.PostalCode)}");
        builder.HasIndex(x => x.City).HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.City)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(CustomerAddress)}_{nameof(CustomerAddress.DeletedAt)}");
        
        //Relations.
        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.DeletedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}