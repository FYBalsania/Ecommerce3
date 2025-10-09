using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public sealed class BankConfiguration : IEntityTypeConfiguration<Bank>
{
    public void Configure(EntityTypeBuilder<Bank> builder)
    {
        //Table.
        builder.ToTable(nameof(Bank));
        
        //PK.
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);
        
        //Properties
        builder.Property(x => x.Name).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(2);
        builder.Property(x => x.Slug).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(3);
        builder.Property(x => x.IsActive).HasColumnType("boolean").HasColumnOrder(4);
        builder.Property(x => x.SortOrder).HasColumnType("integer").HasColumnOrder(5);
        builder.Property(x => x.CreatedBy).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(58);
        
        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);
        
        //Indexes.
        builder.HasIndex(x => x.Name).IsUnique()
            .HasDatabaseName($"UK_{nameof(Bank)}_{nameof(Bank.Name)}");
        builder.HasIndex(x => x.Slug).IsUnique()
            .HasDatabaseName($"UK_{nameof(Bank)}_{nameof(Bank.Slug)}");
        builder.HasIndex(x => x.IsActive).HasDatabaseName($"IX_{nameof(Bank)}_{nameof(Bank.IsActive)}");
        builder.HasIndex(x => x.SortOrder).HasDatabaseName($"IX_{nameof(Bank)}_{nameof(Bank.SortOrder)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(Bank)}_{nameof(Bank.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(Bank)}_{nameof(Bank.DeletedAt)}");
        
        //Relations.
        builder.HasOne(x => (AppUser?)x.CreatedByUser)
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => (AppUser?)x.UpdatedByUser)
            .WithMany()
            .HasForeignKey(x => x.UpdatedBy)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => (AppUser?)x.DeletedByUser)
            .WithMany()
            .HasForeignKey(x => x.DeletedBy)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

    }
}