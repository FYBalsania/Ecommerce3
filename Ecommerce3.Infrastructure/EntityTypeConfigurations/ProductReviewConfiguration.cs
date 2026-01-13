using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductReviewConfiguration : IEntityTypeConfiguration<ProductReview>
{
    public void Configure(EntityTypeBuilder<ProductReview> builder)
    {
        //Table.
        builder.ToTable(nameof(ProductReview));

        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);
        
        //Filters
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Properties.
        builder.Property(x => x.ProductId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.Rating).HasColumnType("decimal(18,2)").HasColumnOrder(3);
        builder.Property(x => x.Review).HasMaxLength(18432).HasColumnType("citext").HasColumnOrder(4);
        builder.Property(x => x.SortOrder).HasColumnType("integer").HasColumnOrder(5);
        builder.Property(x => x.Approver).HasColumnType("integer").HasColumnOrder(6);
        builder.Property(x => x.ApprovedOn).HasColumnType("timestamp").HasColumnOrder(7);
        builder.Property(x => x.ApproverIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(8);
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasColumnName("created_by_ip").HasColumnType("inet").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnName("updated_by").HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnName("updated_at").HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasColumnName("updated_by_ip").HasColumnType("inet").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnName("deleted_by").HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnName("deleted_at").HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasColumnName("deleted_by_ip").HasColumnType("inet").HasColumnOrder(58);

        //Indexes.
        builder.HasIndex(x => x.Rating).HasDatabaseName($"IX_{nameof(ProductReview)}_{nameof(ProductReview.Rating)}");
        builder.HasIndex(x => x.Review).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(ProductReview)}_{nameof(ProductReview.Review)}");
        builder.HasIndex(x => x.SortOrder)
            .HasDatabaseName($"IX_{nameof(ProductReview)}_{nameof(ProductReview.SortOrder)}");
        builder.HasIndex(x => x.ApprovedOn)
            .HasDatabaseName($"IX_{nameof(ProductReview)}_{nameof(ProductReview.ApprovedOn)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"idx_product_review_created_at");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"idx_product_review_deleted_at");
        
        //Relations
        builder.HasOne(x=>x.Product)
            .WithMany(x=>x.Reviews)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => (AppUser?)x.ApproverUser)
            .WithMany()
            .HasForeignKey(x => x.Approver)
            .OnDelete(DeleteBehavior.Restrict);
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