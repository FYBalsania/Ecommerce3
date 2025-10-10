using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public class Page : EntityWithImages<PageImage>, ICreatable, IUpdatable, IDeletable
{
    public string Discriminator { get; private set; }
    public string? Path { get; private set; }
    public string MetaTitle { get; private set; }
    public string? MetaDescription { get; private set; }
    public string? MetaKeywords { get; private set; }
    public string? MetaRobots { get; private set; }
    public string? H1 { get; private set; }
    public string? CanonicalUrl { get; private set; }
    public string? OgTitle { get; private set; }
    public string? OgDescription { get; private set; }
    public string? OgImageUrl { get; private set; }
    public string? OgType { get; private set; } //website, product, article.
    public string? TwitterCard { get; private set; }
    public string? ContentHtml { get; private set; }
    public string? Summary { get; private set; }
    public string? SchemaJsonLd { get; private set; }
    public string? BreadcrumbsJson { get; private set; }
    public string? HreflangMapJson { get; private set; }
    public decimal SitemapPriority { get; private set; }
    public SiteMapFrequency SitemapFrequency { get; private set; }
    public string? RedirectFromJson { get; private set; }
    public bool IsIndexed { get; private set; }
    public string? HeaderScripts { get; private set; }
    public string? FooterScripts { get; private set; }
    public string Language { get; private set; } = "en";
    public string? Region { get; private set; } = "UK";
    public int? SeoScore { get; private set; }
    public bool IsActive { get; private set; }
    public int? BrandId { get; private set; }
    public int? CategoryId { get; private set; }
    public int? ProductId { get; private set; }
    public int? ProductGroupId { get; private set; }
    public int CreatedBy { get; private set; }
    public IAppUser? CreatedByUser { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }

    internal Page()
    {
    }

    public Page(string? path, string metaTitle, string? metaDescription, string? metaKeywords, string? metaRobots,
        string? h1, string? canonicalUrl, string? ogTitle, string? ogDescription, string? ogImageUrl, string? ogType,
        string? twitterCard, string? contentHtml, string? summary, string? schemaJsonLd, string? breadcrumbsJson,
        string? hreflangMapJson, decimal sitemapPriority, SiteMapFrequency sitemapFrequency, string? redirectFromJson,
        bool isIndexed, string? headerScripts, string? footerScripts, string language, string? region, int? seoScore,
        bool isActive, int createdBy, DateTime createdAt, string createdByIp)
    {
        Path = path;
        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
        MetaKeywords = metaKeywords;
        MetaRobots = metaRobots;
        H1 = h1;
        CanonicalUrl = canonicalUrl;
        OgTitle = ogTitle;
        OgDescription = ogDescription;
        OgImageUrl = ogImageUrl;
        OgType = ogType;
        TwitterCard = twitterCard;
        ContentHtml = contentHtml;
        Summary = summary;
        SchemaJsonLd = schemaJsonLd;
        BreadcrumbsJson = breadcrumbsJson;
        HreflangMapJson = hreflangMapJson;
        SitemapPriority = sitemapPriority;
        SitemapFrequency = sitemapFrequency;
        RedirectFromJson = redirectFromJson;
        IsIndexed = isIndexed;
        HeaderScripts = headerScripts;
        FooterScripts = footerScripts;
        Language = language;
        Region = region;
        SeoScore = seoScore;
        IsActive = isActive;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    public bool Update(string? path, string metaTitle, string? metaDescription, string? metaKeywords,
        string? metaRobots, string? h1, string? canonicalUrl, string? ogTitle, string? ogDescription,
        string? ogImageUrl, string? ogType, string? twitterCard, string? contentHtml, string? summary,
        string? schemaJsonLd, string? breadcrumbsJson, string? hreflangMapJson, decimal sitemapPriority,
        SiteMapFrequency sitemapFrequency, string? redirectFromJson, bool isIndexed, string? headerScripts,
        string? footerScripts, string language, string? region, int? seoScore, bool isActive, int updatedBy,
        DateTime updatedAt, string updatedByIp)
    {
        if (Path == path && MetaTitle == metaTitle && MetaDescription == metaDescription &&
            MetaKeywords == metaKeywords && MetaRobots == metaRobots && H1 == h1 &&
            CanonicalUrl == canonicalUrl && OgTitle == ogTitle && OgDescription == ogDescription &&
            OgImageUrl == ogImageUrl && OgType == ogType && TwitterCard == twitterCard &&
            ContentHtml == contentHtml && Summary == summary && SchemaJsonLd == schemaJsonLd &&
            BreadcrumbsJson == breadcrumbsJson && HreflangMapJson == hreflangMapJson &&
            SitemapPriority == sitemapPriority && SitemapFrequency == sitemapFrequency &&
            RedirectFromJson == redirectFromJson && IsIndexed == isIndexed && HeaderScripts == headerScripts &&
            FooterScripts == footerScripts && Language == language && Region == region &&
            SeoScore == seoScore && IsActive == isActive) return false;

        Path = path;
        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
        MetaKeywords = metaKeywords;
        MetaRobots = metaRobots;
        H1 = h1;
        CanonicalUrl = canonicalUrl;
        OgTitle = ogTitle;
        OgDescription = ogDescription;
        OgImageUrl = ogImageUrl;
        OgType = ogType;
        TwitterCard = twitterCard;
        ContentHtml = contentHtml;
        Summary = summary;
        SchemaJsonLd = schemaJsonLd;
        BreadcrumbsJson = breadcrumbsJson;
        HreflangMapJson = hreflangMapJson;
        SitemapPriority = sitemapPriority;
        SitemapFrequency = sitemapFrequency;
        RedirectFromJson = redirectFromJson;
        IsIndexed = isIndexed;
        HeaderScripts = headerScripts;
        FooterScripts = footerScripts;
        Language = language;
        Region = region;
        SeoScore = seoScore;
        IsActive = isActive;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;

        return true;
    }

    public bool Update(string metaTitle, string? metaDescription, string? metaKeywords, string? h1, int updatedBy,
        DateTime updatedAt, string updatedByIp)
    {
        if (MetaTitle == metaTitle && MetaDescription == metaDescription && MetaKeywords == metaKeywords && H1 == h1)
            return false;

        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
        MetaKeywords = metaKeywords;
        H1 = h1;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;

        return true;
    }
}