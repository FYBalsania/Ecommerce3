namespace Ecommerce3.Domain.Entities;

public class Page : EntityWithImages, ICreatable, IUpdatable, IDeletable
{
    public string Discriminator { get; private set; }
    public string? Path { get; private set; }
    public string Title { get; private set; }
    public string MetaTitle { get; private set; }
    public string MetaDescription { get; private set; }
    public string MetaKeywords { get; private set; }
    public string MetaRobots { get; private set; }
    public string CanonicalUrl { get; private set; }
    public string OgTitle { get; private set; }
    public string OgDescription { get; private set; }
    public string OgImageUrl { get; private set; }
    public string OgType { get; private set; } //website, product, article.
    public string TwitterCard { get; private set; }
    public string? ContentHtml { get; private set; }
    public string H1 { get; private set; }
    public string? Summary { get; private set; }
    public string? SchemaJsonLd { get; private set; }
    public string? BreadcrumbsJson { get; private set; }
    public string? HreflangMapJson { get; private set; }
    public decimal SitemapPriority { get; private set; }
    public string SitemapFrequency { get; private set; }
    public string? RedirectFromJson { get; private set; }
    public bool IsIndexed { get; private set; }
    public string? HeaderScripts { get; private set; }
    public string? FooterScripts { get; private set; }
    public string Language { get; private set; }
    public string? Region { get; private set; }
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
}