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
        
        //PK
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().HasColumnOrder(1);
        
        //Properties.
        builder.Property(x => x.Path).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(2);
        builder.Property(x => x.Title).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(3);
        builder.Property(x => x.MetaTitle).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(4);
        builder.Property(x => x.MetaDescription).HasMaxLength(2048).HasColumnType("varchar(2048)").HasColumnOrder(5);
        builder.Property(x => x.MetaKeywords).HasMaxLength(1024).HasColumnType("varchar(1024)").HasColumnOrder(6);
        builder.Property(x => x.MetaRobots).HasMaxLength(32).HasColumnType("varchar(32)").HasColumnOrder(7);
        builder.Property(x => x.CanonicalUrl).HasMaxLength(2048).HasColumnType("varchar(2048)").HasColumnOrder(8);
        builder.Property(x => x.OgTitle).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(9);
        builder.Property(x => x.OgDescription).HasMaxLength(2048).HasColumnType("varchar(2048)").HasColumnOrder(10);
        builder.Property(x => x.OgImageUrl).HasMaxLength(2048).HasColumnType("varchar(2048)").HasColumnOrder(11);
        builder.Property(x => x.OgType).HasMaxLength(16).HasColumnType("varchar(16)").HasColumnOrder(12);
        builder.Property(x => x.TwitterCard).HasMaxLength(64).HasColumnType("varchar(64)").HasColumnOrder(13);
        builder.Property(x => x.ContentHtml).HasColumnType("text").HasColumnOrder(14);
        builder.Property(x => x.H1).HasMaxLength(256).HasColumnType("varchar(256)").HasColumnOrder(15);
        builder.Property(x => x.Summary).HasMaxLength(1024).HasColumnType("varchar(1024)").HasColumnOrder(16);
        builder.Property(x => x.SchemaJsonLd).HasColumnType("text").HasColumnOrder(17);
        builder.Property(x => x.BreadcrumbsJson).HasColumnType("text").HasColumnOrder(18);
        builder.Property(x => x.HreflangMapJson).HasColumnType("text").HasColumnOrder(19);
        builder.Property(x => x.SitemapPriority).HasColumnType("decimal(18,2)").HasColumnOrder(20);
        builder.Property(x => x.SitemapFrequency).HasMaxLength(16).HasColumnType("varchar(16)").HasColumnOrder(21);
        builder.Property(x => x.RedirectFromJson).HasColumnType("text").HasColumnOrder(22);
        builder.Property(x => x.IsIndexed).HasColumnType("boolean").HasColumnOrder(23);
        builder.Property(x => x.HeaderScripts).HasColumnType("text").HasColumnOrder(24);
        builder.Property(x => x.FooterScripts).HasColumnType("text").HasColumnOrder(25);
        builder.Property(x => x.Language).HasMaxLength(8).HasColumnType("varchar(8)").HasColumnOrder(26);
        builder.Property(x => x.Region).HasMaxLength(8).HasColumnType("varchar(8)").HasColumnOrder(27);
        builder.Property(x => x.SeoScore).HasColumnType("integer").HasColumnOrder(28);
        builder.Property(x => x.IsActive).HasColumnType("boolean").HasColumnOrder(29);
        builder.Property(x => x.CreatedBy).HasColumnType("integer").HasColumnOrder(50);
        builder.Property(x => x.CreatedAt).HasColumnType("timestamp").HasColumnOrder(51);
        builder.Property(x => x.CreatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(52);
        builder.Property(x => x.UpdatedBy).HasColumnType("integer").HasColumnOrder(53);
        builder.Property(x => x.UpdatedAt).HasColumnType("timestamp").HasColumnOrder(54);
        builder.Property(x => x.UpdatedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(55);
        builder.Property(x => x.DeletedBy).HasColumnType("integer").HasColumnOrder(56);
        builder.Property(x => x.DeletedAt).HasColumnType("timestamp").HasColumnOrder(57);
        builder.Property(x => x.DeletedByIp).HasMaxLength(128).HasColumnType("varchar(128)").HasColumnOrder(58);
        
        //Navigation.
        builder.Navigation(x => x.Images).HasField("_images").UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.Navigation(x => x.Tags).HasField("_tags").UsePropertyAccessMode(PropertyAccessMode.Field);
        
        //Indexes.
        builder.HasIndex(x => x.Path).IsUnique().HasDatabaseName($"UK_{nameof(Page)}_{nameof(Page.Path)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(Page)}_{nameof(Page.CreatedAt)}");
        builder.HasIndex(x => x.IsIndexed).HasDatabaseName($"IX_{nameof(Page)}_{nameof(Page.IsIndexed)}");
        builder.HasIndex(x => x.IsActive).HasDatabaseName($"IX_{nameof(Page)}_{nameof(Page.IsActive)}");
        builder.HasIndex(x => x.CreatedAt).HasDatabaseName($"IX_{nameof(Page)}_{nameof(Page.CreatedAt)}");
        builder.HasIndex(x => x.DeletedAt).HasDatabaseName($"IX_{nameof(Page)}_{nameof(Page.DeletedAt)}");
        
        //Relations.
        builder.HasMany(x => x.Images)
            .WithOne()
            .HasForeignKey(x => x.PageId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.Tags)
            .WithMany(x => x.Pages);
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