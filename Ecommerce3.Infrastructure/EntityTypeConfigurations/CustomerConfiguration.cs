using Ecommerce3.Domain.Entities;
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
        
        //Navigation Properties.
        builder.Navigation(x => x.History).HasField("_history").UsePropertyAccessMode(PropertyAccessMode.Field);

        //Properties.
        builder.Property(x => x.FirstName).HasMaxLength(64).HasColumnType("citext").HasColumnOrder(2);
        builder.Property(x => x.LastName).HasMaxLength(64).HasColumnType("citext").HasColumnOrder(3);
        builder.Property(x => x.CompanyName).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(4);
        builder.Property(x => x.EmailAddress).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(5);
        builder.Property(x => x.PhoneNumber).HasMaxLength(64).HasColumnType("citext").HasColumnOrder(6);
        builder.Property(x => x.Password).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(7);
        builder.Property(x => x.IsEmailVerified).HasColumnType("boolean").HasColumnOrder(8);
        builder.Property(x => x.PasswordResetToken).HasMaxLength(512).HasColumnType("varchar(512)").HasColumnOrder(9);
        builder.Property(x => x.PasswordResetTokenExpiry).HasColumnType("timestamp").HasColumnOrder(10);
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
            nb.Property(x => x.FirstName).HasMaxLength(64).HasColumnType("citext").HasColumnOrder(1);
            nb.Property(x => x.LastName).HasMaxLength(64).HasColumnType("citext").HasColumnOrder(2);
            nb.Property(x => x.CompanyName).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(3);
            nb.Property(x => x.PhoneNumber).HasMaxLength(64).HasColumnType("citext").HasColumnOrder(4);
            nb.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(5);
            nb.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(6);
        });

        //Indexes.
        builder.HasIndex(x => x.FirstName).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Customer)}_{nameof(Customer.FirstName)}");
        builder.HasIndex(x => x.LastName).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Customer)}_{nameof(Customer.LastName)}");
        builder.HasIndex(x => x.CompanyName).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Customer)}_{nameof(Customer.CompanyName)}");
        builder.HasIndex(x => x.EmailAddress).IsUnique()
            .HasDatabaseName($"UK_{nameof(Customer)}_{nameof(Customer.EmailAddress)}");
        builder.HasIndex(x => x.PhoneNumber).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"UK_{nameof(Customer)}_{nameof(Customer.PhoneNumber)}");
        builder.HasIndex(x => x.PasswordResetToken)
            .HasDatabaseName($"IX_{nameof(Customer)}_{nameof(Customer.PasswordResetToken)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(Customer)}_{nameof(Customer.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(Customer)}_{nameof(Customer.DeletedAt)}");

        //Relations.
        builder.HasMany(x => x.Addresses)
            .WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}