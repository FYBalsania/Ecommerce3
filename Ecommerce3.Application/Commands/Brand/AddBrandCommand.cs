using System.Net;

namespace Ecommerce3.Application.Commands.Brand;

public record AddBrandCommand
{
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public required string Display { get; init; }
    public required string Breadcrumb { get; init; }
    public required string AnchorText { get; init; }
    public string? AnchorTitle { get; init; }
    public required string MetaTitle { get; init; }
    public string? MetaDescription { get; init; }
    public string? MetaKeywords { get; init; }
    public required string H1 { get; init; }
    public string? ShortDescription { get; init; }
    public string? FullDescription { get; init; }
    public bool IsActive { get; init; }
    public int SortOrder { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public required IPAddress CreatedByIp { get; init; }
}