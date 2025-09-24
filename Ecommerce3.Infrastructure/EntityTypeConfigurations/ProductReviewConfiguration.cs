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
        builder.Property(x => x.Review).HasMaxLength(18432).HasColumnType("varchar(18432)").HasColumnOrder(4);
        builder.Property(x => x.SortOrder).HasColumnType("integer").HasColumnOrder(5);
        builder.Property(x => x.Approver).HasColumnType("integer").HasColumnOrder(6);
        builder.Property(x => x.ApprovedOn).HasColumnType("timestamp").HasColumnOrder(7);
        builder.Property(x => x.ApproverIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(8);
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
        builder.HasIndex(x => x.SortOrder)
            .HasDatabaseName($"IX_{nameof(ProductReview)}_{nameof(ProductReview.SortOrder)}");
        builder.HasIndex(x => x.ApprovedOn)
            .HasDatabaseName($"IX_{nameof(ProductReview)}_{nameof(ProductReview.ApprovedOn)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(ProductReview)}_{nameof(ProductReview.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(ProductReview)}_{nameof(ProductReview.DeletedAt)}");
        
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