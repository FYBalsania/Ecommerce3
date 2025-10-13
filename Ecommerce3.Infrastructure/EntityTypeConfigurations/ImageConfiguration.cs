using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        //Table.
        builder.ToTable(nameof(Image));

        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);

        //Discriminator.
        builder.HasDiscriminator(x => x.Discriminator)
            .HasValue<Image>(nameof(Image))
            .HasValue<BrandImage>(nameof(BrandImage))
            .HasValue<CategoryImage>(nameof(CategoryImage))
            .HasValue<ProductImage>(nameof(ProductImage))
            .HasValue<PageImage>(nameof(PageImage))
            .HasValue<ProductGroupImage>(nameof(ProductGroupImage));

        //Filters
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Properties.
        builder.Property(x => x.Discriminator).HasMaxLength(32).HasColumnType("varchar(32)").HasColumnOrder(2);
        builder.Property(x => x.OgFileName).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(3);
        builder.Property(x => x.FileName).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(4);
        builder.Property(x => x.FileExtension).HasMaxLength(8).HasColumnType("varchar(8)").HasColumnOrder(5);
        builder.Property(x => x.ImageTypeId).HasColumnType("integer").HasColumnOrder(6);
        builder.Property(x => x.Size).HasConversion<string>().HasMaxLength(8).HasColumnType("varchar(8)")
            .HasColumnOrder(7);
        builder.Property(x => x.AltText).HasMaxLength(128).HasColumnType("citext").HasColumnOrder(8);
        builder.Property(x => x.Title).HasMaxLength(128).HasColumnType("citext").HasColumnOrder(9);
        builder.Property(x => x.Loading).HasMaxLength(8).HasColumnType("varchar(8)").HasColumnOrder(10);
        builder.Property(x => x.Link).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(11);
        builder.Property(x => x.LinkTarget).HasMaxLength(8).HasColumnType("varchar(8)").HasColumnOrder(12);
        builder.Property(x => x.SortOrder).HasColumnType("integer").HasColumnOrder(18);
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
        builder.HasIndex(x => x.Discriminator).HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.Discriminator)}");
        builder.HasIndex(x => x.OgFileName).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.OgFileName)}");
        builder.HasIndex(x => x.FileName).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.FileName)}");
        builder.HasIndex(x => x.AltText).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.AltText)}");
        builder.HasIndex(x => x.Title).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.Title)}");
        builder.HasIndex(x => x.SortOrder).HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.SortOrder)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(Image)}_{nameof(Image.DeletedAt)}");

        //Relations
        builder.HasOne(x => x.ImageType)
            .WithMany()
            .HasForeignKey(x => x.ImageTypeId)
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