using Ecommerce3.Data.Entities;
using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Data.EntityTypeConfigurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        //Table.
        builder.ToTable(nameof(Image));

        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Properties.
        builder.Property(x => x.OgFileName).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(2);
        builder.Property(x => x.FileName).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(3);
        builder.Property(x => x.FileExtension).HasMaxLength(8).HasColumnType("varchar(8)").HasColumnOrder(4);
        builder.Property(x => x.ImageTypeId).HasColumnType("integer").HasColumnOrder(5);
        builder.Property(x => x.Size).HasConversion<string>().HasMaxLength(8).HasColumnType("varchar(8)")
            .HasColumnOrder(6);
        builder.Property(x => x.AltText).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(7);
        builder.Property(x => x.Title).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(8);
        builder.Property(x => x.Loading).HasMaxLength(8).HasColumnType("varchar(8)").HasColumnOrder(9);
        builder.Property(x => x.Link).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(10);
        builder.Property(x => x.LinkTarget).HasMaxLength(8).HasColumnType("varchar(8)").HasColumnOrder(11);
        builder.Property(x => x.SortOrder).HasColumnType("integer").HasColumnOrder(12);
        builder.Property(x => x.BrandId).HasColumnType("integer").HasColumnOrder(13);
        builder.Property(x => x.ProductId).HasColumnType("integer").HasColumnOrder(14);
        builder.Property(x => x.CategoryId).HasColumnType("integer").HasColumnOrder(15);
        builder.Property(x => x.PageId).HasColumnType("integer").HasColumnOrder(16);
        builder.Property(x => x.ProductGroupId).HasColumnType("integer").HasColumnOrder(17);
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
        builder.HasIndex(x => x.FileName).HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.FileName)}");
        builder.HasIndex(x => new { x.BrandId, x.SortOrder })
            .HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.BrandId)}_{nameof(Image.SortOrder)}");
        builder.HasIndex(x => new { x.ProductId, x.SortOrder })
            .HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.ProductId)}_{nameof(Image.SortOrder)}");
        builder.HasIndex(x => new { x.CategoryId, x.SortOrder })
            .HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.CategoryId)}_{nameof(Image.SortOrder)}");
        builder.HasIndex(x => new { x.PageId, x.SortOrder })
            .HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.PageId)}_{nameof(Image.SortOrder)}");
        builder.HasIndex(x => new { x.ProductGroupId, x.SortOrder })
            .HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.ProductGroupId)}_{nameof(Image.SortOrder)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.DeletedAt)}");

        //Relations
        builder.HasOne<ImageType>()
            .WithMany()
            .HasForeignKey(x => x.ImageTypeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Brand>()
            .WithMany()
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Page>()
            .WithMany()
            .HasForeignKey(x => x.PageId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<ProductGroup>()
            .WithMany()
            .HasForeignKey(x => x.ProductGroupId)
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