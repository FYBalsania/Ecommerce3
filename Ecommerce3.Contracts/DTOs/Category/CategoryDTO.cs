namespace Ecommerce3.Contracts.DTOs.Category;

public class CategoryDTO
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Display { get; set; }
    public string Breadcrumb { get; set; }
    public string AnchorText { get; set; }
    public string? AnchorTitle { get; set; }
    public string? GoogleCategory  { get; set; }
    public string Path { get; set; }
    public string MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
    public string H1 { get; set; }
    public string? ShortDescription { get; set; }
    public string? FullDescription { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
}