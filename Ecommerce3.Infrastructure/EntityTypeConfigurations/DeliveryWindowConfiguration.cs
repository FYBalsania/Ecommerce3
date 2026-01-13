using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class DeliveryWindowConfiguration : IEntityTypeConfiguration<DeliveryWindow>
{
    public void Configure(EntityTypeBuilder<DeliveryWindow> builder)
    {
        //Table.
        builder.ToTable(nameof(DeliveryWindow));

        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Properties.
        builder.Property(x => x.Name).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(2);
        builder.Property(x => x.Unit).HasConversion<string>().HasMaxLength(8).HasColumnType("varchar(8)").HasColumnOrder(3);
        builder.Property(x => x.MinValue).HasColumnType("integer").HasColumnOrder(4);
        builder.Property(x => x.MaxValue).HasColumnType("integer").HasColumnOrder(5);
        builder.Property(x => x.NormalizedMinDays).HasColumnType("decimal(18,1)").HasColumnOrder(6);
        builder.Property(x => x.NormalizedMaxDays).HasColumnType("decimal(18,1)").HasColumnOrder(7);
        builder.Property(x => x.SortOrder).HasColumnType("integer").HasColumnOrder(8);
        builder.Property(x => x.IsActive).HasColumnType("boolean").HasColumnOrder(9);
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
        builder.HasIndex(x => x.Name).IsUnique()
            .HasDatabaseName($"UK_{nameof(DeliveryWindow)}_{nameof(DeliveryWindow.Name)}");
        builder.HasIndex(x => new { x.Name, x.SortOrder, x.IsActive})
            .HasDatabaseName($"IX_{nameof(DeliveryWindow)}_{nameof(DeliveryWindow.Name)}_{nameof(DeliveryWindow.SortOrder)}_{nameof(DeliveryWindow.IsActive)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"idx_delivery_window_created_at");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"idx_delivery_window_deleted_at");
        
        //Relations.
        builder.HasOne(x => (AppUser?)x.CreatedByUser)
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => (AppUser?)x.UpdatedByUser)
            .WithMany()
            .HasForeignKey(x => x.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => (AppUser?)x.DeletedByUser)
            .WithMany()
            .HasForeignKey(x => x.DeletedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}