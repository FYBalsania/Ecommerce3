using Ecommerce3.Contracts.DTOs.Image;

namespace Ecommerce3.Contracts.DTOs.ProductGroup;

public class ProductGroupDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Display { get; set; }
    public string Breadcrumb { get; set; }
    public string AnchorText { get; set; }
    public string? AnchorTitle { get; set; }
    public string MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
    public string H1 { get; set; }
    public string? ShortDescription { get; set; }
    public string? FullDescription { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
    public IReadOnlyList<ImageDTO> Images { get; private set; } = [];
}