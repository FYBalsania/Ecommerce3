using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public class Page : EntityWithImages<PageImage>, ICreatable, IUpdatable, IDeletable
{
    public override string ImageNamePrefix => Path;
    public string Discriminator { get; private set; }
    public string Path { get; private set; }
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
    public int? BankId { get; private set; }
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

    public Page(string path, string metaTitle, string? metaDescription, string? metaKeywords, string? metaRobots,
        string? h1, string? canonicalUrl, string? ogTitle, string? ogDescription, string? ogImageUrl, string? ogType,
        string? twitterCard, string? contentHtml, string? summary, string? schemaJsonLd, string? breadcrumbsJson,
        string? hreflangMapJson, decimal sitemapPriority, SiteMapFrequency sitemapFrequency, string? redirectFromJson,
        bool isIndexed, string? headerScripts, string? footerScripts, string language, string? region, int? seoScore,
        bool isActive, int createdBy, DateTime createdAt, string createdByIp)
    {
        ValidatePath(path);
        ValidateMetaTitle(metaTitle);
        ValidateMetaDescription(metaDescription);
        ValidateMetaKeywords(metaKeywords);
        ValidateMetaRobots(metaRobots);
        ValidateH1(h1);
        ValidateCanonicalUrl(canonicalUrl);
        ValidateOgTitle(ogTitle);
        ValidateOgDescription(ogDescription);
        ValidateOgImageUrl(ogImageUrl);
        ValidateOgType(ogType);
        ValidateTwitterCard(twitterCard);
        ValidateSummary(summary);
        ValidateLanguage(language);
        ValidateRegion(region);
        ValidateCreatedBy(createdBy);
        ValidateCreatedByIp(createdByIp);

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

    private static void ValidateCreatedByIp(string createdByIp)
    {
        if (string.IsNullOrWhiteSpace(createdByIp)) throw new DomainException(DomainErrors.PageErrors.CreatedByIpRequired);
        if (createdByIp.Length > 128) throw new DomainException(DomainErrors.PageErrors.CreatedByIpTooLong);
    }

    private static void ValidateCreatedBy(int createdBy)
    {
        if (createdBy <= 0) throw new DomainException(DomainErrors.PageErrors.InvalidCreatedBy);
    }

    private static void ValidateRegion(string? region)
    {
        if(region?.Length > 8) throw new DomainException(DomainErrors.PageErrors.RegionTooLong);
    }

    private static void ValidateLanguage(string language)
    {
        if(string.IsNullOrWhiteSpace(language)) throw new DomainException(DomainErrors.PageErrors.LanguageRequired);
        if(language.Length > 8) throw new DomainException(DomainErrors.PageErrors.LanguageTooLong);
    }

    private static void ValidateSummary(string? summary)
    {
        if (summary?.Length > 1024) throw new DomainException(DomainErrors.PageErrors.SummaryTooLong);
    }

    private static void ValidateTwitterCard(string? twitterCard)
    {
        if (twitterCard?.Length > 64) throw new DomainException(DomainErrors.PageErrors.TwitterCardTooLong);
    }

    private static void ValidateOgType(string? ogType)
    {
        if (ogType?.Length > 16) throw new DomainException(DomainErrors.PageErrors.OgTypeTooLong);
    }

    private static void ValidateOgImageUrl(string? ogImageUrl)
    {
        if (!string.IsNullOrWhiteSpace(ogImageUrl) && !Uri.TryCreate(ogImageUrl, UriKind.Relative, out _))
            throw new DomainException(DomainErrors.PageErrors.InvalidOgImageUrl);
        if (ogImageUrl?.Length > 2048) throw new DomainException(DomainErrors.PageErrors.OgImageUrlTooLong);
    }

    private static void ValidateOgDescription(string? ogDescription)
    {
        if (ogDescription?.Length > 2048) throw new DomainException(DomainErrors.PageErrors.OgDescriptionTooLong);
    }

    private static void ValidateOgTitle(string? ogTitle)
    {
        if (ogTitle?.Length > 256) throw new DomainException(DomainErrors.PageErrors.OgTitleTooLong);
    }

    private static void ValidateCanonicalUrl(string? canonicalUrl)
    {
        if (!string.IsNullOrWhiteSpace(canonicalUrl) && !Uri.TryCreate(canonicalUrl, UriKind.Relative, out _))
            throw new DomainException(DomainErrors.PageErrors.InvalidCanonicalUrl);
        if (canonicalUrl?.Length > 2048) throw new DomainException(DomainErrors.PageErrors.CanonicalUrlTooLong);
    }

    private static void ValidateH1(string? h1)
    {
        if (h1?.Length > 256) throw new DomainException(DomainErrors.PageErrors.H1TooLong);
    }

    private static void ValidateMetaRobots(string? metaRobots)
    {
        if (metaRobots?.Length > 32) throw new DomainException(DomainErrors.PageErrors.MetaRobotsTooLong);
    }

    private static void ValidateMetaKeywords(string? metaKeywords)
    {
        if (metaKeywords?.Length > 1024) throw new DomainException(DomainErrors.PageErrors.MetaKeywordsTooLong);
    }

    private static void ValidateMetaDescription(string? metaDescription)
    {
        if (metaDescription?.Length > 2048) throw new DomainException(DomainErrors.PageErrors.MetaDescriptionTooLong);
    }

    private static void ValidateMetaTitle(string metaTitle)
    {
        if (string.IsNullOrWhiteSpace(metaTitle)) throw new DomainException(DomainErrors.PageErrors.MetaTitleRequired);
        if (metaTitle.Length > 256) throw new DomainException(DomainErrors.PageErrors.MetaTitleTooLong);
    }

    private static void ValidatePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) throw new DomainException(DomainErrors.PageErrors.PathRequired);
        if (!Uri.TryCreate(path, UriKind.Relative, out _))
            throw new DomainException(DomainErrors.PageErrors.InvalidPath);
        if (path.Length > 256) throw new DomainException(DomainErrors.PageErrors.PathTooLong);
    }
    
    private static void ValidateUpdatedBy(int updatedBy)
    {
        if (updatedBy <= 0) throw new DomainException(DomainErrors.PageErrors.InvalidUpdatedBy);
    }
    
    private static void ValidateUpdatedByIp(string updatedByIp)
    {
        if (string.IsNullOrWhiteSpace(updatedByIp)) throw new DomainException(DomainErrors.PageErrors.UpdatedByIpRequired);
        if (updatedByIp.Length > 128) throw new DomainException(DomainErrors.PageErrors.UpdatedByIpTooLong);
    }

    public bool Update(string path, string metaTitle, string? metaDescription, string? metaKeywords,
        string? metaRobots, string? h1, string? canonicalUrl, string? ogTitle, string? ogDescription,
        string? ogImageUrl, string? ogType, string? twitterCard, string? contentHtml, string? summary,
        string? schemaJsonLd, string? breadcrumbsJson, string? hreflangMapJson, decimal sitemapPriority,
        SiteMapFrequency sitemapFrequency, string? redirectFromJson, bool isIndexed, string? headerScripts,
        string? footerScripts, string language, string? region, int? seoScore, bool isActive, int updatedBy,
        DateTime updatedAt, string updatedByIp)
    {
        ValidatePath(path);
        ValidateMetaTitle(metaTitle);
        ValidateMetaDescription(metaDescription);
        ValidateMetaKeywords(metaKeywords);
        ValidateMetaRobots(metaRobots);
        ValidateH1(h1);
        ValidateCanonicalUrl(canonicalUrl);
        ValidateOgTitle(ogTitle);
        ValidateOgDescription(ogDescription);
        ValidateOgImageUrl(ogImageUrl);
        ValidateOgType(ogType);
        ValidateTwitterCard(twitterCard);
        ValidateSummary(summary);
        ValidateLanguage(language);
        ValidateRegion(region);
        ValidateUpdatedBy(updatedBy);
        ValidateUpdatedByIp(updatedByIp);
        
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
        ValidateMetaTitle(metaTitle);
        ValidateMetaDescription(metaDescription);
        ValidateMetaKeywords(metaKeywords);
        ValidateH1(h1);
        ValidateUpdatedBy(updatedBy);
        ValidateUpdatedByIp(updatedByIp);
        
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
    
    public void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }
}