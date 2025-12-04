using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class UnitOfMeasureConfiguration : IEntityTypeConfiguration<UnitOfMeasure>
{
    public void Configure(EntityTypeBuilder<UnitOfMeasure> builder)
    {
        //Table.
        builder.ToTable(nameof(UnitOfMeasure));

        //PK.
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Properties.
        builder.Property(x => x.Code).HasMaxLength(UnitOfMeasure.CodeMaxLength)
            .HasColumnType($"varchar({UnitOfMeasure.CodeMaxLength})").HasColumnOrder(2);
        builder.Property(x => x.Name).HasMaxLength(UnitOfMeasure.NameMaxLength)
            .HasColumnType($"varchar({UnitOfMeasure.NameMaxLength})").HasColumnOrder(3);
        builder.Property(x => x.Type).HasConversion<string>().HasMaxLength(UnitOfMeasure.TypeMaxLength)
            .HasColumnType($"varchar({UnitOfMeasure.TypeMaxLength})").HasColumnOrder(4);
        builder.Property(x => x.BaseId).HasColumnType("integer").HasColumnOrder(5);
        builder.Property(x => x.ConversionFactor).HasColumnType("decimal(18,3)").HasColumnOrder(6);
        builder.Property(x => x.IsActive).HasColumnType("boolean").HasColumnOrder(7);
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
        builder.HasIndex(x => x.Code).IsUnique()
            .HasDatabaseName($"UK_{nameof(UnitOfMeasure)}_{nameof(UnitOfMeasure.Code)}");
        builder.HasIndex(x => x.Name).IsUnique()
            .HasDatabaseName($"UK_{nameof(UnitOfMeasure)}_{nameof(UnitOfMeasure.Name)}");
        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName($"IX_{nameof(UnitOfMeasure)}_{nameof(UnitOfMeasure.IsActive)}");
        builder.HasIndex(x => x.CreatedAt)
            .HasDatabaseName($"IX_{nameof(UnitOfMeasure)}_{nameof(UnitOfMeasure.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt)
            .HasDatabaseName($"IX_{nameof(UnitOfMeasure)}_{nameof(UnitOfMeasure.DeletedAt)}");

        //Relations.
        builder.HasOne(x => x.Base)
            .WithMany()
            .HasForeignKey(x => x.BaseId)
            .OnDelete(DeleteBehavior.Restrict);
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