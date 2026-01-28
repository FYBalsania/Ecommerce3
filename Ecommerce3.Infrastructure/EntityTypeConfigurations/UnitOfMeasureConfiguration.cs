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
        builder.ToTable("unit_of_measures");

        //PK.
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnName("id").HasColumnOrder(1);

        //Properties.
        builder.Property(x => x.Code).HasMaxLength(UnitOfMeasure.CodeMaxLength)
            .HasColumnType($"varchar({UnitOfMeasure.CodeMaxLength})").HasColumnName("code").HasColumnOrder(2);

        builder.Property(x => x.SingularName).HasMaxLength(UnitOfMeasure.NameMaxLength)
            .HasColumnType($"varchar({UnitOfMeasure.NameMaxLength})").HasColumnName("singular_name").HasColumnOrder(3);

        builder.Property(x => x.PluralName).HasMaxLength(UnitOfMeasure.NameMaxLength)
            .HasColumnType($"varchar({UnitOfMeasure.NameMaxLength})").HasColumnName("plural_name").HasColumnOrder(4);

        builder.Property(x => x.Type).HasConversion<string>().HasMaxLength(UnitOfMeasure.TypeMaxLength)
            .HasColumnType($"varchar({UnitOfMeasure.TypeMaxLength})").HasColumnName("type").HasColumnOrder(5);

        builder.Property(x => x.BaseId).HasColumnType("integer").HasColumnName("base_id").HasColumnOrder(6);

        builder.Property(x => x.ConversionFactor).HasColumnType("decimal(20,8)").HasColumnName("conversion_factor")
            .HasColumnOrder(7);

        builder.Property(x => x.DecimalPlaces).HasColumnType("smallint").HasColumnName("decimal_places")
            .HasColumnOrder(8);

        builder.Property(x => x.IsActive).HasColumnType("boolean").HasColumnName("is_active").HasColumnOrder(9);
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
            .HasDatabaseName("uk_unit_of_measures_code");
        builder.HasIndex(x => x.SingularName).IsUnique()
            .HasDatabaseName("uk_unit_of_measures_singular_name");
        builder.HasIndex(x => x.PluralName).IsUnique()
            .HasDatabaseName("uk_unit_of_measures_plural_name");
        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_unit_of_measures_is_active");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"ix_unit_of_measure_deleted_at");

        //Relations.
        builder.HasOne(x => x.Base)
            .WithOne()
            .HasForeignKey<UnitOfMeasure>(x => x.BaseId)
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