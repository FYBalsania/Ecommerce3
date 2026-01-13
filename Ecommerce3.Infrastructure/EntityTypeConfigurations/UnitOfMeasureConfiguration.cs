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
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasColumnName("created_by_ip").HasColumnType("inet").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasColumnName("updated_by_ip").HasColumnType("inet").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnName("deleted_by").HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnName("deleted_at").HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasColumnName("deleted_by_ip").HasColumnType("inet").HasColumnOrder(58);

        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Indexes.
        builder.HasIndex(x => x.Code).IsUnique()
            .HasDatabaseName($"UK_{nameof(UnitOfMeasure)}_{nameof(UnitOfMeasure.Code)}");
        builder.HasIndex(x => x.Name).IsUnique()
            .HasDatabaseName($"UK_{nameof(UnitOfMeasure)}_{nameof(UnitOfMeasure.Name)}");
        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName($"IX_{nameof(UnitOfMeasure)}_{nameof(UnitOfMeasure.IsActive)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"idx_unit_of_measure_created_at");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"idx_unit_of_measure_deleted_at");

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