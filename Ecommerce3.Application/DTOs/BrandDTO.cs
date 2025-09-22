namespace Ecommerce3.Application.DTOs;

public class BrandDTO
{
    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string Display { get; private set; }
    public string Breadcrumb { get; private set; }
    public string AnchorText { get; private set; }
    public string? AnchorTitle { get; private set; }
    public string MetaTitle { get; private set; }
    public string? MetaDescription { get; private set; }
    public string? MetaKeywords { get; private set; }
    public string H1 { get; private set; }
    public string? ShortDescription { get; private set; }
    public string? FullDescription { get; private set; }
    public bool IsActive { get; private set; }
    public int SortOrder { get; private set; }
    public string CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public string? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public string? DeletedBy { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }
}