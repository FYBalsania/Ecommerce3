using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class Product : EntityWithImages
{
    private readonly List<ProductCategory> _categories = [];
    private readonly List<ProductTextListItem> _textListItems = [];
    private readonly List<ProductKVPListItem> _kvpListItems = [];
    private readonly List<ProductQnA> _qnas = [];
    private readonly List<ProductReview> _reviews = [];
    private readonly List<ProductProductAttribute> _attributes = [];
    
    public string SKUCode { get; private set; }
    public string? GTIN { get; private set; }
    public string? MPN { get; private set; }
    public string? MFC { get; private set; }
    public string? EAN { get; private set; }
    public string? UPC { get; private set; }
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
    public int? BrandId { get; private set; }
    public int? ProductGroupId { get; private set; }
    public string? ShortDescription { get; private set; }
    public string? FullDescription { get; private set; }
    public bool AllowReviews { get; private set; }
    public int AverageRating { get; private set; }
    public int TotalReviews { get; private set; }
    public decimal Price { get; private set; }
    public decimal? OldPrice { get; private set; }
    public decimal? CostPrice { get; private set; }
    public int Stock { get; private set; }
    public int MinStock { get; private set; }
    public bool ShowAvailability { get; private set; }
    public bool FreeShipping { get; private set; }
    public decimal AdditionalShippingCharge { get; private set; }
    public decimal WeightKgs { get; private set; }
    public int DeliveryWindowId { get; private set; }
    public int MinOrderQuantity { get; private set; }
    public int? MaxOrderQuantity { get; private set; }
    public bool Featured { get; private set; }
    public bool New { get; private set; }
    public bool BestSeller { get; private set; }
    public bool Returnable { get; private set; }
    public string? ReturnPolicy { get; private set; }
    public ProductStatus Status { get; private set; }
    public string? RedirectUrl { get; private set; }
    public int SortOrder { get; private set; }
    
    public IReadOnlyList<ProductCategory> Categories => _categories;
    public IReadOnlyList<ProductTextListItem> TextListItems => _textListItems;
    public IReadOnlyList<ProductKVPListItem> Faqs => _kvpListItems;
    public IReadOnlyList<ProductQnA> QnAs => _qnas;
    public IReadOnlyList<ProductReview> Reviews => _reviews;
    public IReadOnlyList<ProductProductAttribute> Attributes => _attributes;
}