using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ProductAttributeConfiguration : IEntityTypeConfiguration<ProductAttribute>
{
    public void Configure(EntityTypeBuilder<ProductAttribute> builder)
    {
        //Table.
        builder.ToTable(nameof(ProductAttribute));

        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Properties.
        builder.Property(x => x.Name).HasMaxLength(ProductAttribute.NameMaxLength).HasColumnType("citext")
            .HasColumnOrder(2);
        builder.Property(x => x.Slug).HasMaxLength(ProductAttribute.SlugMaxLength).HasColumnType("citext")
            .HasColumnOrder(3);
        builder.Property(x => x.Display).HasMaxLength(ProductAttribute.DisplayMaxLength).HasColumnType("citext")
            .HasColumnOrder(4);
        builder.Property(x => x.Breadcrumb).HasMaxLength(ProductAttribute.BreadcrumbMaxLength).HasColumnType("citext")
            .HasColumnOrder(5);
        builder.Property(x => x.DataType).HasConversion<string>().HasMaxLength(16).HasColumnType("varchar(16)")
            .HasColumnOrder(6);
        builder.Property(x => x.SortOrder).HasColumnType("decimal(18,2)").HasColumnOrder(7);
        builder.Property(x => x.CreatedBy).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasMaxLength(ICreatable.CreatedByIpMaxLength)
            .HasColumnType($"varchar({ICreatable.CreatedByIpMaxLength})").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(IUpdatable.UpdatedByIpMaxLength)
            .HasColumnType($"varchar({IUpdatable.UpdatedByIpMaxLength})").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasMaxLength(IDeletable.DeletedByIpMaxLength)
            .HasColumnType($"varchar({IDeletable.DeletedByIpMaxLength})").HasColumnOrder(58);

        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Navigation.
        builder.Navigation(x => x.Values).HasField("_values").UsePropertyAccessMode(PropertyAccessMode.Field);

        //Indexes.
        builder.HasIndex(x => x.Name).IsUnique()
            .HasDatabaseName($"UK_{nameof(ProductAttribute)}_{nameof(ProductAttribute.Name)}");
        builder.HasIndex(x => x.Slug).IsUnique()
            .HasDatabaseName($"UK_{nameof(ProductAttribute)}_{nameof(ProductAttribute.Slug)}");
        builder.HasIndex(x => x.Display).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(ProductAttribute)}_{nameof(ProductAttribute.Display)}");
        builder.HasIndex(x => x.Breadcrumb).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(ProductAttribute)}_{nameof(ProductAttribute.Breadcrumb)}");
        builder.HasIndex(x => x.SortOrder)
            .HasDatabaseName($"IX_{nameof(ProductAttribute)}_{nameof(ProductAttribute.SortOrder)}");
        builder.HasIndex(x => x.CreatedAt)
            .HasDatabaseName($"IX_{nameof(ProductAttribute)}_{nameof(ProductAttribute.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt)
            .HasDatabaseName($"IX_{nameof(ProductAttribute)}_{nameof(ProductAttribute.DeletedAt)}");

        //Relations.
        builder.HasMany(x => x.Values)
            .WithOne(x => x.ProductAttribute)
            .HasForeignKey(x => x.ProductAttributeId)
            .OnDelete(DeleteBehavior.Cascade);
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