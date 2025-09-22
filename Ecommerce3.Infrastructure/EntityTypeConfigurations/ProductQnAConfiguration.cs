using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductQnAConfiguration : IEntityTypeConfiguration<ProductQnA>
{
    public void Configure(EntityTypeBuilder<ProductQnA> builder)
    {
        //Table.
        builder.ToTable(nameof(ProductQnA));

        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Properties.
        builder.Property(x => x.ProductId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.Question).HasMaxLength(2048).HasColumnType("varchar(2048)").HasColumnOrder(3);
        builder.Property(x => x.SortOrder).HasColumnType("integer").HasColumnOrder(4);
        builder.Property(x => x.Answer).HasColumnType("text").HasColumnOrder(5);
        builder.Property(x => x.AnsweredBy).HasColumnType("integer").HasColumnOrder(6);
        builder.Property(x => x.AnsweredOn).HasColumnType("timestamp").HasColumnOrder(7);
        builder.Property(x => x.AnswererIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(8);
        builder.Property(x => x.Approver).HasColumnType("integer").HasColumnOrder(9);
        builder.Property(x => x.ApprovedOn).HasColumnType("timestamp").HasColumnOrder(10);
        builder.Property(x => x.ApproverIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(11);
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
        builder.HasIndex(x => x.SortOrder).HasDatabaseName($"IX_{nameof(ProductQnA)}_{nameof(ProductQnA.SortOrder)}");
        builder.HasIndex(x => x.AnsweredOn).HasDatabaseName($"IX_{nameof(ProductQnA)}_{nameof(ProductQnA.AnsweredOn)}");
        builder.HasIndex(x => x.ApprovedOn).HasDatabaseName($"IX_{nameof(ProductQnA)}_{nameof(ProductQnA.ApprovedOn)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(ProductQnA)}_{nameof(ProductQnA.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(ProductQnA)}_{nameof(ProductQnA.DeletedAt)}");
        
        //Relations.
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.AnsweredBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.Approver)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<AppUser>()
            .WithMany()
            .HasForeignKey(x => x.DeletedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}