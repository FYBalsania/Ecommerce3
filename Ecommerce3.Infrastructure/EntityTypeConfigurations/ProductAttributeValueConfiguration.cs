using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductAttributeValueConfiguration : IEntityTypeConfiguration<ProductAttributeValue>
{
    public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
    {
        //Table.
        builder.ToTable(nameof(ProductAttributeValue));

        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Discriminator.
        builder.HasDiscriminator(x => x.Discriminator)
            .HasValue<ProductAttributeValue>(nameof(ProductAttributeValue))
            .HasValue<ProductAttributeColourValue>(nameof(ProductAttributeColourValue));

        //Properties.
        builder.Property(x => x.ProductAttributeId).HasColumnType("integer").HasColumnOrder(2);
        builder.Property(x => x.Discriminator).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(3);
        builder.Property(x => x.Value).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(4);
        builder.Property(x => x.Slug).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(5);
        builder.Property(x => x.Display).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(6);
        builder.Property(x => x.Breadcrumb).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(7);
        builder.Property(x => x.NumberValue).HasColumnType("decimal(18,3)").HasColumnOrder(8);
        builder.Property(x => x.BooleanValue).HasColumnType("boolean").HasColumnOrder(9);
        builder.Property(x => x.DateOnlyValue).HasColumnType("date").HasColumnOrder(10);
        builder.Property(x => x.SortOrder).HasColumnType("integer").HasColumnOrder(11);
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
        builder.HasIndex(x => x.Discriminator)
            .HasDatabaseName($"IX_{nameof(ProductAttributeValue)}_{nameof(ProductAttributeValue.Discriminator)}");
        builder.HasIndex(x => new { x.ProductAttributeId, x.Value })
            .IsUnique()
            .HasDatabaseName($"UK_{nameof(ProductAttributeValue)}_{nameof(ProductAttributeValue.ProductAttributeId)}_{nameof(ProductAttributeValue.Value)}");
        builder.HasIndex(x => x.Slug)
            .HasDatabaseName($"IX_{nameof(ProductAttributeValue)}_{nameof(ProductAttributeValue.Slug)}");
        builder.HasIndex(x => new { x.ProductAttributeId, x.SortOrder, x.Value })
            .HasDatabaseName($"IX_{nameof(ProductAttributeValue)}_{nameof(ProductAttributeValue.ProductAttributeId)}_{nameof(ProductAttributeValue.SortOrder)}_{nameof(ProductAttributeValue.Value)}");

        //Relations.
        builder.HasOne(x => x.ProductAttribute)
            .WithMany(x => x.Values)
            .HasForeignKey(x => x.ProductAttributeId)
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