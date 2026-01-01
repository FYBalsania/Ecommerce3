using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Commands.Page;

public record AddPageCommand
{
    public required string Type { get; init; }
    public string? Path { get; init; }
    public string? H1 { get; init; }
    public string? CanonicalUrl { get; init; }
    public required string MetaTitle { get; init; }
    public string? MetaDescription { get; init; }
    public string? MetaKeywords { get; init; }
    public string? MetaRobots { get; init; }
    public string? OgTitle { get; init; }
    public string? OgDescription { get; init; }
    public string? OgImageUrl { get; init; }
    public string? OgType { get; init; }
    public string? TwitterCard { get; init; }
    public string? ContentHtml { get; init; }
    public string? Summary { get; init; }
    public string? SchemaJsonLd { get; init; }
    public string? BreadcrumbsJson { get; init; }
    public string? HreflangMapJson { get; init; }
    public int? SeoScore { get; init; }
    public required decimal SitemapPriority { get; init; }
    public required SiteMapFrequency SitemapFrequency { get; init; }
    public string? RedirectFromJson { get; init; }
    public required bool IsIndexed { get; init; }
    public required bool IsActive { get; init; }
    public required string Language { get; init; }
    public string? Region { get; init; }
    public string? HeaderScripts { get; init; }
    public string? FooterScripts { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public required string CreatedByIp { get; init; }
}