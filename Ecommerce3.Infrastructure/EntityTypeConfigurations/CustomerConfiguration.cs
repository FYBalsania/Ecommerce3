using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        //Table.
        builder.ToTable(nameof(Customer));

        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Navigation.
        builder.Navigation(x => x.Addresses).HasField("_addresses").UsePropertyAccessMode(PropertyAccessMode.Field);

        //Properties.
        builder.Property(x => x.FirstName).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(2);
        builder.Property(x => x.LastName).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(3);
        builder.Property(x => x.CompanyName).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(4);
        builder.Property(x => x.EmailAddress).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(5);
        builder.Property(x => x.PhoneNumber).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(6);
        builder.Property(x => x.Password).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(7);
        builder.Property(x => x.IsEmailVerified).HasColumnType("boolean").HasColumnOrder(8);
        builder.Property(x => x.PasswordResetToken).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(9);
        builder.Property(x => x.PasswordResetTokenExpiry).HasColumnType("timestamp").HasColumnOrder(10);
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
        builder.HasIndex(x => x.FirstName).HasDatabaseName($"IX_{nameof(Customer)}_{nameof(Customer.FirstName)}");
        builder.HasIndex(x => x.LastName).HasDatabaseName($"IX_{nameof(Customer)}_{nameof(Customer.LastName)}");
        builder.HasIndex(x => x.CompanyName).HasDatabaseName($"IX_{nameof(Customer)}_{nameof(Customer.CompanyName)}");
        builder.HasIndex(x => x.EmailAddress).IsUnique().HasDatabaseName($"UK_{nameof(Customer)}_{nameof(Customer.EmailAddress)}");
        builder.HasIndex(x => x.PhoneNumber).HasDatabaseName($"UK_{nameof(Customer)}_{nameof(Customer.PhoneNumber)}");
        builder.HasIndex(x => x.PasswordResetToken).HasDatabaseName($"IX_{nameof(Customer)}_{nameof(Customer.PasswordResetToken)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(Customer)}_{nameof(Customer.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(Customer)}_{nameof(Customer.DeletedAt)}");

        //Relations.
        builder.HasMany(x => x.Addresses)
            .WithOne()
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