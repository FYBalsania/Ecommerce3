using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce3.Infrastructure.Extensions;

public static class PageExtensions
{
    public static IQueryable<BrandPageDTO> ProjectToDTO(this IQueryable<BrandPage> query)
    {
        return query.Select(bp => new BrandPageDTO
        {
            Id = bp.Id,
            Path = bp.Path,
            IsActive = bp.IsActive,
            H1 = bp.H1,
            MetaTitle = bp.MetaTitle,
            MetaDescription = bp.MetaDescription,
            MetaKeywords = bp.MetaKeywords,
            MetaRobots = bp.MetaRobots,
            CanonicalUrl = bp.CanonicalUrl,
            OgTitle = bp.OgTitle,
            OgDescription = bp.OgDescription,
            OgImageUrl = bp.OgImageUrl,
            OgType = bp.OgType,
            TwitterCard = bp.TwitterCard,
            ContentHtml = bp.ContentHtml,
            Summary = bp.Summary,
            SchemaJsonLd = bp.SchemaJsonLd,
            BreadcrumbsJson = bp.BreadcrumbsJson,
            HreflangMapJson = bp.HreflangMapJson,
            SitemapPriority = bp.SitemapPriority,
            SitemapFrequency = bp.SitemapFrequency,
            RedirectFromJson = bp.RedirectFromJson,
            IsIndexed = bp.IsIndexed,
            HeaderScripts = bp.HeaderScripts,
            FooterScripts = bp.FooterScripts,
            Language = bp.Language,
            Region = bp.Region,
            SeoScore = bp.SeoScore,
            PageType = EF.Property<string>(bp, "Discriminator"),
            BrandId = bp.Brand!.Id,
            BrandName = bp.Brand!.Name,
            Images = bp.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
                .Select(ImageExtensions.DTOExpression).ToList()
        });
    }

    public static IQueryable<CategoryPageDTO> ProjectToDTO(this IQueryable<CategoryPage> query)
    {
        return query.Select(cp => new CategoryPageDTO
        {
            Id = cp.Id,
            Path = cp.Path,
            IsActive = cp.IsActive,
            H1 = cp.H1,
            MetaTitle = cp.MetaTitle,
            MetaDescription = cp.MetaDescription,
            MetaKeywords = cp.MetaKeywords,
            MetaRobots = cp.MetaRobots,
            CanonicalUrl = cp.CanonicalUrl,
            OgTitle = cp.OgTitle,
            OgDescription = cp.OgDescription,
            OgImageUrl = cp.OgImageUrl,
            OgType = cp.OgType,
            TwitterCard = cp.TwitterCard,
            ContentHtml = cp.ContentHtml,
            Summary = cp.Summary,
            SchemaJsonLd = cp.SchemaJsonLd,
            BreadcrumbsJson = cp.BreadcrumbsJson,
            HreflangMapJson = cp.HreflangMapJson,
            SitemapPriority = cp.SitemapPriority,
            SitemapFrequency = cp.SitemapFrequency,
            RedirectFromJson = cp.RedirectFromJson,
            IsIndexed = cp.IsIndexed,
            HeaderScripts = cp.HeaderScripts,
            FooterScripts = cp.FooterScripts,
            Language = cp.Language,
            Region = cp.Region,
            SeoScore = cp.SeoScore,
            PageType = EF.Property<string>(cp, "Discriminator"),
            CategoryId = cp.Category!.Id,
            CategoryName = cp.Category!.Name,
            Images = cp.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
                .Select(ImageExtensions.DTOExpression).ToList()
        });
    }
    
    public static IQueryable<BankPageDTO> ProjectToDTO(this IQueryable<BankPage> query)
    {
        return query.Select(bkp => new BankPageDTO
        {
            Id = bkp.Id,
            Path = bkp.Path,
            IsActive = bkp.IsActive,
            H1 = bkp.H1,
            MetaTitle = bkp.MetaTitle,
            MetaDescription = bkp.MetaDescription,
            MetaKeywords = bkp.MetaKeywords,
            MetaRobots = bkp.MetaRobots,
            CanonicalUrl = bkp.CanonicalUrl,
            OgTitle = bkp.OgTitle,
            OgDescription = bkp.OgDescription,
            OgImageUrl = bkp.OgImageUrl,
            OgType = bkp.OgType,
            TwitterCard = bkp.TwitterCard,
            ContentHtml = bkp.ContentHtml,
            Summary = bkp.Summary,
            SchemaJsonLd = bkp.SchemaJsonLd,
            BreadcrumbsJson = bkp.BreadcrumbsJson,
            HreflangMapJson = bkp.HreflangMapJson,
            SitemapPriority = bkp.SitemapPriority,
            SitemapFrequency = bkp.SitemapFrequency,
            RedirectFromJson = bkp.RedirectFromJson,
            IsIndexed = bkp.IsIndexed,
            HeaderScripts = bkp.HeaderScripts,
            FooterScripts = bkp.FooterScripts,
            Language = bkp.Language,
            Region = bkp.Region,
            SeoScore = bkp.SeoScore,
            PageType = EF.Property<string>(bkp, "Discriminator"),
            BankId = bkp.Bank!.Id,
            BankName = bkp.Bank!.Name,
            Images = bkp.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
                .Select(ImageExtensions.DTOExpression).ToList()
        });
    }
    
    public static IQueryable<ProductPageDTO> ProjectToDTO(this IQueryable<ProductPage> query)
    {
        return query.Select(pp => new ProductPageDTO
        {
            Id = pp.Id,
            Path = pp.Path,
            IsActive = pp.IsActive,
            H1 = pp.H1,
            MetaTitle = pp.MetaTitle,
            MetaDescription = pp.MetaDescription,
            MetaKeywords = pp.MetaKeywords,
            MetaRobots = pp.MetaRobots,
            CanonicalUrl = pp.CanonicalUrl,
            OgTitle = pp.OgTitle,
            OgDescription = pp.OgDescription,
            OgImageUrl = pp.OgImageUrl,
            OgType = pp.OgType,
            TwitterCard = pp.TwitterCard,
            ContentHtml = pp.ContentHtml,
            Summary = pp.Summary,
            SchemaJsonLd = pp.SchemaJsonLd,
            BreadcrumbsJson = pp.BreadcrumbsJson,
            HreflangMapJson = pp.HreflangMapJson,
            SitemapPriority = pp.SitemapPriority,
            SitemapFrequency = pp.SitemapFrequency,
            RedirectFromJson = pp.RedirectFromJson,
            IsIndexed = pp.IsIndexed,
            HeaderScripts = pp.HeaderScripts,
            FooterScripts = pp.FooterScripts,
            Language = pp.Language,
            Region = pp.Region,
            SeoScore = pp.SeoScore,
            PageType = EF.Property<string>(pp, "Discriminator"),
            ProductId = pp.Product!.Id,
            ProductName = pp.Product!.Name,
            Images = pp.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
                .Select(ImageExtensions.DTOExpression).ToList()
        });
    }
    
    public static IQueryable<ProductGroupPageDTO> ProjectToDTO(this IQueryable<ProductGroupPage> query)
    {
        return query.Select(pgp => new ProductGroupPageDTO
        {
            Id = pgp.Id,
            Path = pgp.Path,
            IsActive = pgp.IsActive,
            H1 = pgp.H1,
            MetaTitle = pgp.MetaTitle,
            MetaDescription = pgp.MetaDescription,
            MetaKeywords = pgp.MetaKeywords,
            MetaRobots = pgp.MetaRobots,
            CanonicalUrl = pgp.CanonicalUrl,
            OgTitle = pgp.OgTitle,
            OgDescription = pgp.OgDescription,
            OgImageUrl = pgp.OgImageUrl,
            OgType = pgp.OgType,
            TwitterCard = pgp.TwitterCard,
            ContentHtml = pgp.ContentHtml,
            Summary = pgp.Summary,
            SchemaJsonLd = pgp.SchemaJsonLd,
            BreadcrumbsJson = pgp.BreadcrumbsJson,
            HreflangMapJson = pgp.HreflangMapJson,
            SitemapPriority = pgp.SitemapPriority,
            SitemapFrequency = pgp.SitemapFrequency,
            RedirectFromJson = pgp.RedirectFromJson,
            IsIndexed = pgp.IsIndexed,
            HeaderScripts = pgp.HeaderScripts,
            FooterScripts = pgp.FooterScripts,
            Language = pgp.Language,
            Region = pgp.Region,
            SeoScore = pgp.SeoScore,
            PageType = EF.Property<string>(pgp, "Discriminator"),
            ProductGroupId = pgp.ProductGroup!.Id,
            ProductGroupName = pgp.ProductGroup!.Name,
            Images = pgp.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
                .Select(ImageExtensions.DTOExpression).ToList()
        });
    }
    
    public static IQueryable<BrandCategoryPageDTO> ProjectToDTO(this IQueryable<BrandCategoryPage> query)
    {
        return query.Select(bcp => new BrandCategoryPageDTO
        {
            Id = bcp.Id,
            Path = bcp.Path,
            IsActive = bcp.IsActive,
            H1 = bcp.H1,
            MetaTitle = bcp.MetaTitle,
            MetaDescription = bcp.MetaDescription,
            MetaKeywords = bcp.MetaKeywords,
            MetaRobots = bcp.MetaRobots,
            CanonicalUrl = bcp.CanonicalUrl,
            OgTitle = bcp.OgTitle,
            OgDescription = bcp.OgDescription,
            OgImageUrl = bcp.OgImageUrl,
            OgType = bcp.OgType,
            TwitterCard = bcp.TwitterCard,
            ContentHtml = bcp.ContentHtml,
            Summary = bcp.Summary,
            SchemaJsonLd = bcp.SchemaJsonLd,
            BreadcrumbsJson = bcp.BreadcrumbsJson,
            HreflangMapJson = bcp.HreflangMapJson,
            SitemapPriority = bcp.SitemapPriority,
            SitemapFrequency = bcp.SitemapFrequency,
            RedirectFromJson = bcp.RedirectFromJson,
            IsIndexed = bcp.IsIndexed,
            HeaderScripts = bcp.HeaderScripts,
            FooterScripts = bcp.FooterScripts,
            Language = bcp.Language,
            Region = bcp.Region,
            SeoScore = bcp.SeoScore,
            PageType = EF.Property<string>(bcp, "Discriminator"),
            BrandId = bcp.Brand!.Id,
            BrandName = bcp.Brand!.Name,
            CategoryId = bcp.Category!.Id,
            CategoryName = bcp.Category!.Name,
            Images = bcp.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
                .Select(ImageExtensions.DTOExpression).ToList()
        });
    }
    
    public static IQueryable<PageDTO> ProjectToDTO(this IQueryable<Page> query)
    {
        return query.Select(pgd => new PageDTO
        {
            Id = pgd.Id,
            Path = pgd.Path,
            IsActive = pgd.IsActive,
            H1 = pgd.H1,
            MetaTitle = pgd.MetaTitle,
            MetaDescription = pgd.MetaDescription,
            MetaKeywords = pgd.MetaKeywords,
            MetaRobots = pgd.MetaRobots,
            CanonicalUrl = pgd.CanonicalUrl,
            OgTitle = pgd.OgTitle,
            OgDescription = pgd.OgDescription,
            OgImageUrl = pgd.OgImageUrl,
            OgType = pgd.OgType,
            TwitterCard = pgd.TwitterCard,
            ContentHtml = pgd.ContentHtml,
            Summary = pgd.Summary,
            SchemaJsonLd = pgd.SchemaJsonLd,
            BreadcrumbsJson = pgd.BreadcrumbsJson,
            HreflangMapJson = pgd.HreflangMapJson,
            SitemapPriority = pgd.SitemapPriority,
            SitemapFrequency = pgd.SitemapFrequency,
            RedirectFromJson = pgd.RedirectFromJson,
            IsIndexed = pgd.IsIndexed,
            HeaderScripts = pgd.HeaderScripts,
            FooterScripts = pgd.FooterScripts,
            Language = pgd.Language,
            Region = pgd.Region,
            SeoScore = pgd.SeoScore,
            PageType = EF.Property<string>(pgd, "Discriminator"),
            Images = pgd.Images.AsQueryable().OrderBy(y => y.ImageType!.Slug).ThenBy(z => z.SortOrder)
                .Select(ImageExtensions.DTOExpression).ToList()
        });
    }
}