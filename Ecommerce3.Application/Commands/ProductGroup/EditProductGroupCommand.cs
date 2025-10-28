namespace Ecommerce3.Application.Commands.ProductGroup;

public record EditProductGroupCommand
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Slug { get; init; }
    public string Display { get; init; }
    public string Breadcrumb { get; init; }
    public string AnchorText { get; init; }
    public string? AnchorTitle { get; init; }
    public string MetaTitle { get; init; }
    public string? MetaDescription { get; init; }
    public string? MetaKeywords { get; init; }
    public string H1 { get; init; }
    public string? ShortDescription { get; init; }
    public string? FullDescription { get; init; }
    public bool IsActive { get; init; }
    public int SortOrder { get; init; }
    public int UpdatedBy { get; init; }
    public DateTime UpdatedAt { get; init; }
    public string UpdatedByIp { get; init; }
}