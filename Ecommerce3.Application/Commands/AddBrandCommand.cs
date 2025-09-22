namespace Ecommerce3.Application.Commands;

public class AddBrandCommand
{
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public required string Display { get; init; }
    public required string Breadcrumb { get; init; }
    public required string AnchorText { get; init; }
    public required string? AnchorTitle { get; init; }
    public required string MetaTitle { get; init; }
    public required string? MetaDescription { get; init; }
    public required string? MetaKeywords { get; init; }
    public required string H1 { get; init; }
    public required string? ShortDescription { get; init; }
    public required string? FullDescription { get; init; }
    public required bool IsActive { get; init; }
    public required int SortOrder { get; init; }
    public required int CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string CreatedByIp { get; init; }
}