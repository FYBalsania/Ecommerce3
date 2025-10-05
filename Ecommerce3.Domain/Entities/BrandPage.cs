using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class BrandPage : Page
{
    public Brand? Brand { get; private set; }

    internal BrandPage() : base()
    {
    }

    public BrandPage(string? path, string metaTitle, string? metaDescription, string? metaKeywords, string? metaRobots,
        string? h1, string? canonicalUrl, string? ogTitle, string? ogDescription, string? ogImageUrl, string? ogType,
        string? twitterCard, string? contentHtml, string? summary, string? schemaJsonLd, string? breadcrumbsJson,
        string? hreflangMapJson, decimal sitemapPriority, SiteMapFrequency sitemapFrequency, string? redirectFromJson,
        bool isIndexed, string? headerScripts, string? footerScripts, string language, string? region, int? seoScore,
        bool isActive, int createdBy, DateTime createdAt, string createdByIp, Brand brand)
        : base(path, metaTitle, metaDescription, metaKeywords, metaRobots, h1, canonicalUrl, ogTitle, ogDescription,
            ogImageUrl, ogType, twitterCard, contentHtml, summary, schemaJsonLd, breadcrumbsJson, hreflangMapJson,
            sitemapPriority, sitemapFrequency, redirectFromJson, isIndexed, headerScripts, footerScripts, language,
            region, seoScore, isActive, createdBy, createdAt, createdByIp)
    {
        Brand = brand;
    }
}