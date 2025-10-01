namespace Ecommerce3.Application.Commands.Brand;

public record EditBrandCommand
{
    public int Id { get; set; }
    public string Name { get; set; }
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