using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ImageTypeConfiguration : IEntityTypeConfiguration<ImageType>
{
    public void Configure(EntityTypeBuilder<ImageType> builder)
    {
        //Table.
        builder.ToTable(nameof(ImageType));

        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Properties.
        builder.Property(x => x.Entity).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(2);
        builder.Property(x => x.Name).HasMaxLength(128).HasColumnType("citext").HasColumnOrder(3);
        builder.Property(x => x.Slug).HasMaxLength(128).HasColumnType("citext").HasColumnOrder(4);
        builder.Property(x => x.Description).HasMaxLength(1024).HasColumnType("varchar(1024)").HasColumnOrder(5);
        builder.Property(x => x.IsActive).HasColumnType("boolean").HasColumnOrder(6);
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
        builder.HasIndex(x => x.Entity).HasDatabaseName($"IX_{nameof(ImageType)}_{nameof(ImageType.Entity)}");
        builder.HasIndex(x => x.Name).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(ImageType)}_{nameof(ImageType.Name)}");
        builder.HasIndex(x => x.Slug).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(ImageType)}_{nameof(ImageType.Slug)}");
        builder.HasIndex(x => new { x.Entity, Type = x.Name }).IsUnique()
            .HasDatabaseName($"UK_{nameof(ImageType)}_{nameof(ImageType.Entity)}_{nameof(ImageType.Name)}");
        builder.HasIndex(x => new { x.Entity, Type = x.Slug }).IsUnique()
            .HasDatabaseName($"UK_{nameof(ImageType)}_{nameof(ImageType.Entity)}_{nameof(ImageType.Slug)}");
        builder.HasIndex(x => x.IsActive).HasDatabaseName($"IX_{nameof(ImageType)}_{nameof(ImageType.IsActive)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"idx_image_type_created_at");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"idx_image_type_deleted_at");
        
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