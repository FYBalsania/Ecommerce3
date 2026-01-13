using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public sealed class PostCodeConfiguration : IEntityTypeConfiguration<PostCode>
{
    public void Configure(EntityTypeBuilder<PostCode> builder)
    {
        //Table.
        builder.ToTable(nameof(PostCode));
        
        //Key.
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);
        
        //Properties
        builder.Property(x => x.Code).HasMaxLength(16).HasColumnType("varchar(16)").HasColumnOrder(2);
        builder.Property(x => x.IsActive).HasColumnType("boolean").HasColumnOrder(3);
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
            .HasDatabaseName($"UK_{nameof(PostCode)}_{nameof(PostCode.Code)}");
        builder.HasIndex(x => x.IsActive).HasDatabaseName($"IX_{nameof(PostCode)}_{nameof(PostCode.IsActive)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"idx_postcode_created_at");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"idx_postcode_deleted_at");
        
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