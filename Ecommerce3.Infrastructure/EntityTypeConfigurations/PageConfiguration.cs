using Ecommerce3.Domain.Entities;
using Ecommerce3.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce3.Infrastructure.EntityTypeConfigurations;

public class PageConfiguration : IEntityTypeConfiguration<Page>
{
    public void Configure(EntityTypeBuilder<Page> builder)
    {
        //Table.
        builder.ToTable(nameof(Page));

        //PK.
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);
        
        //Navigation Properties.
        builder.Navigation(x => x.Images).HasField("_images").UsePropertyAccessMode(PropertyAccessMode.Field);

        //Discriminator.
        builder.HasDiscriminator(x => x.Discriminator)
            .HasValue<Page>(nameof(Page))
            .HasValue<BrandPage>(nameof(BrandPage))
            .HasValue<CategoryPage>(nameof(CategoryPage))
            .HasValue<ProductPage>(nameof(ProductPage))
            .HasValue<BrandCategoryPage>(nameof(BrandCategoryPage));

        //Filters.
        builder.HasQueryFilter(x => x.DeletedAt == null);

        //Properties.
        builder.Property(x => x.Discriminator).HasMaxLength(32).HasColumnType("varchar(32)").HasColumnOrder(11);
        builder.Property(x => x.Path).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(12);
        builder.Property(x => x.MetaTitle).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(13);
        builder.Property(x => x.MetaDescription).HasMaxLength(2048).HasColumnType("varchar(2048)").HasColumnOrder(14);
        builder.Property(x => x.MetaKeywords).HasMaxLength(1024).HasColumnType("varchar(1024)").HasColumnOrder(15);
        builder.Property(x => x.MetaRobots).HasMaxLength(32).HasColumnType("varchar(32)").HasColumnOrder(16);
        builder.Property(x => x.CanonicalUrl).HasMaxLength(2048).HasColumnType("citext").HasColumnOrder(17);
        builder.Property(x => x.OgTitle).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(18);
        builder.Property(x => x.OgDescription).HasMaxLength(2048).HasColumnType("varchar(2048)").HasColumnOrder(19);
        builder.Property(x => x.OgImageUrl).HasMaxLength(2048).HasColumnType("varchar(2048)").HasColumnOrder(20);
        builder.Property(x => x.OgType).HasMaxLength(16).HasColumnType("varchar(16)").HasColumnOrder(21);
        builder.Property(x => x.TwitterCard).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(22);
        builder.Property(x => x.ContentHtml).HasColumnType("text").HasColumnOrder(23);
        builder.Property(x => x.H1).HasMaxLength(256).HasColumnType("citext").HasColumnOrder(24);
        builder.Property(x => x.Summary).HasMaxLength(1024).HasColumnType("varchar(1024)").HasColumnOrder(25);
        builder.Property(x => x.SchemaJsonLd).HasColumnType("text").HasColumnOrder(26);
        builder.Property(x => x.BreadcrumbsJson).HasColumnType("text").HasColumnOrder(27);
        builder.Property(x => x.HreflangMapJson).HasColumnType("text").HasColumnOrder(28);
        builder.Property(x => x.SitemapPriority).HasColumnType("decimal(18,2)").HasColumnOrder(29);
        builder.Property(x => x.SitemapFrequency).HasMaxLength(16).HasColumnType("varchar(16)").HasColumnOrder(30);
        builder.Property(x => x.RedirectFromJson).HasColumnType("text").HasColumnOrder(31);
        builder.Property(x => x.IsIndexed).HasColumnType("boolean").HasColumnOrder(32);
        builder.Property(x => x.HeaderScripts).HasColumnType("text").HasColumnOrder(33);
        builder.Property(x => x.FooterScripts).HasColumnType("text").HasColumnOrder(34);
        builder.Property(x => x.Language).HasMaxLength(8).HasColumnType("varchar(8)").HasColumnOrder(35);
        builder.Property(x => x.Region).HasMaxLength(8).HasColumnType("varchar(8)").HasColumnOrder(36);
        builder.Property(x => x.SeoScore).HasColumnType("integer").HasColumnOrder(37);
        builder.Property(x => x.BrandId).HasColumnType("integer").HasColumnOrder(38);
        builder.Property(x => x.CategoryId).HasColumnType("integer").HasColumnOrder(39);
        builder.Property(x => x.ProductId).HasColumnType("integer").HasColumnOrder(40);
        builder.Property(x => x.ProductGroupId).HasColumnType("integer").HasColumnOrder(41);
        builder.Property(x => x.IsActive).HasColumnType("boolean").HasColumnOrder(42);
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
        builder.HasIndex(x => x.Path).IsUnique()
            .HasDatabaseName($"UK_{nameof(Page)}_{nameof(Page.Path)}");
        builder.HasIndex(x => x.MetaTitle).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Page)}_{nameof(Page.MetaTitle)}");
        builder.HasIndex(x => x.CanonicalUrl).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Page)}_{nameof(Page.CanonicalUrl)}");
        builder.HasIndex(x => x.H1).HasMethod("gin").HasOperators("gin_trgm_ops")
            .HasDatabaseName($"IX_{nameof(Page)}_{nameof(Page.H1)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(Page)}_{nameof(Page.CreatedAt)}");
        builder.HasIndex(x => x.IsIndexed).HasDatabaseName($"IX_{nameof(Page)}_{nameof(Page.IsIndexed)}");
        builder.HasIndex(x => x.IsActive).HasDatabaseName($"IX_{nameof(Page)}_{nameof(Page.IsActive)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(Page)}_{nameof(Page.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(Page)}_{nameof(Page.DeletedAt)}");

        //Relations.
        builder.HasMany(x => x.Images)
            .WithOne(x => x.Page)
            .HasForeignKey(x => x.PageId)
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