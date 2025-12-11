using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.DTOs.Page;

public record PageDTO
{
    public required int Id { get; init; }
    public required string? Path { get; init; }
    public required string MetaTitle { get; init; }
    public required string? MetaDescription { get; init; }
    public required string? MetaKeywords { get; init; }
    public required string? MetaRobots { get; init; }
    public required string? H1 { get; init; }
    public required string? CanonicalUrl { get; init; }
    public required string? OgTitle { get; init; }
    public required string? OgDescription { get; init; }
    public required string? OgImageUrl { get; init; }
    public required string? OgType { get; init; }
    public required string? TwitterCard { get; init; }
    public required string? ContentHtml { get; init; }
    public required string? Summary { get; init; }
    public required string? SchemaJsonLd { get; init; }
    public required string? BreadcrumbsJson { get; init; }
    public required string? HreflangMapJson { get; init; }
    public required decimal SitemapPriority { get; init; }
    public required SiteMapFrequency SitemapFrequency { get; init; }
    public required string? RedirectFromJson { get; init; }
    public required bool IsIndexed { get; init; }
    public required string? HeaderScripts { get; init; }
    public required string? FooterScripts { get; init; }
    public required string Language { get; init; } = "en";
    public required string? Region { get; init; } = "UK";
    public required int? SeoScore { get; init; }
    public required bool IsActive { get; init; }
}