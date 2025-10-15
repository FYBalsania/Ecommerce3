namespace Ecommerce3.Contracts.Filters;

public sealed record CategoryFilter
{
    public string? Name { get; init; }
    public int? ParentId { get; init; }
    public string? Slug { get; init; }
    public string? Display { get; init; }
    public string? Breadcrumb { get; init; }
    public string? AnchorText { get; init; }
    public string? AnchorTitle { get; init; }
    public bool? IsActive { get; init; }
}