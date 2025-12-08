using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.DTOs.Page;

public record PageDTO
{
    public required int Id { get; init; }
    public required string? Path { get; init; }
    public required string MetaTitle { get; init; }
    public string? MetaDescription { get; init; }
    public string? MetaKeywords { get; init; }
    public string? MetaRobots { get; init; }
    public string? H1 { get; init; }
    public string? CanonicalUrl { get; init; }
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
    public decimal SitemapPriority { get; init; }
    public SiteMapFrequency SitemapFrequency { get; init; }
    public string? RedirectFromJson { get; init; }
    public bool IsIndexed { get; init; }
    public string? HeaderScripts { get; init; }
    public string? FooterScripts { get; init; }
    public string Language { get; init; } = "en";
    public string? Region { get; init; } = "UK";
    public int? SeoScore { get; init; }
    public bool IsActive { get; init; }


}