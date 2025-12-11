using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class Product : EntityWithImages<ProductImage>, ICreatable, IUpdatable, IDeletable,
    IKVPListItems<ProductKVPListItem>
{
    private readonly List<ProductCategory> _categories = [];
    private readonly List<ProductTextListItem> _textListItems = [];
    private readonly List<ProductKVPListItem> _kvpListItems = [];
    private readonly List<ProductQnA> _qnas = [];
    private readonly List<ProductReview> _reviews = [];
    private readonly List<ProductProductAttribute> _attributes = [];
    private readonly List<string> _facets = [];

    public override string ImageNamePrefix => Slug;
    public string SKU { get; private set; }
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
    public int BrandId { get; private set; }
    public Brand? Brand { get; private set; }
    public int? ProductGroupId { get; private set; }
    public ProductGroup? ProductGroup { get; private set; }
    public string? ShortDescription { get; private set; }
    public string? FullDescription { get; private set; }
    public bool AllowReviews { get; private set; }
    public int AverageRating { get; private set; }
    public int TotalReviews { get; private set; }
    public decimal Price { get; private set; }
    public decimal? OldPrice { get; private set; }
    public decimal? CostPrice { get; private set; }
    public decimal Stock { get; private set; }
    public decimal? MinStock { get; private set; }
    public bool ShowAvailability { get; private set; }
    public bool FreeShipping { get; private set; }
    public decimal AdditionalShippingCharge { get; private set; }
    public int UnitOfMeasureId { get; private set; }
    public UnitOfMeasure? UnitOfMeasure { get; private set; }
    public decimal QuantityPerUnitOfMeasure { get; private set; }
    public int DeliveryWindowId { get; private set; }
    public DeliveryWindow? DeliveryWindow { get; private set; }
    public int MinOrderQuantity { get; private set; }
    public int? MaxOrderQuantity { get; private set; }
    public bool IsFeatured { get; private set; }
    public bool IsNew { get; private set; }
    public bool IsBestSeller { get; private set; }
    public bool IsReturnable { get; private set; }
    public ProductStatus Status { get; private set; }
    public string? RedirectUrl { get; private set; }
    public decimal SortOrder { get; private set; }
    public int CreatedBy { get; }
    public IAppUser? CreatedByUser { get; }
    public DateTime CreatedAt { get; }
    public string CreatedByIp { get; }
    public int? UpdatedBy { get; }
    public IAppUser? UpdatedByUser { get; }
    public DateTime? UpdatedAt { get; }
    public string? UpdatedByIp { get; }
    public int? DeletedBy { get; set; }
    public IAppUser? DeletedByUser { get; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedByIp { get; set; }

    public IReadOnlyList<ProductCategory> Categories => _categories;
    public IReadOnlyList<ProductTextListItem> TextListItems => _textListItems;
    public IReadOnlyList<ProductKVPListItem> KVPListItems => _kvpListItems;
    public IReadOnlyList<ProductQnA> QnAs => _qnas;
    public IReadOnlyList<ProductReview> Reviews => _reviews;
    public IReadOnlyList<ProductProductAttribute> Attributes => _attributes;
    public IReadOnlyList<string> Facets => _facets.AsReadOnly();
    public ProductPage? Page { get; private set; }

    private Product()
    {
    }

    public Product(string sku, string? gtin, string? mpn, string? mfc, string? ean, string? upc, string name,
        string slug, string display, string breadcrumb, string anchorText, string? anchorTitle, int brandId,
        int? productGroupId, string? shortDescription, string? fullDescription, bool allowReviews, decimal price,
        decimal? oldPrice, decimal? costPrice, int stock, int minStock, bool showAvailability, bool freeShipping,
        decimal additionalShippingCharge, int unitOfMeasureId, decimal quantityPerUnitOfMeasure,
        int deliveryWindowId, int minOrderQuantity, int? maxOrderQuantity, bool isFeatured, bool isNew,
        bool isBestSeller, bool isReturnable, ProductStatus status, string? redirectUrl,
        decimal sortOrder, int createdBy, DateTime createdAt, string createdByIp)
    {
        SKU = sku;
        GTIN = gtin;
        MPN = mpn;
        MFC = mfc;
        EAN = ean;
        UPC = upc;
        Name = name;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        AnchorText = anchorText;
        AnchorTitle = anchorTitle;
        BrandId = brandId;
        ProductGroupId = productGroupId;
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        AllowReviews = allowReviews;
        Price = price;
        OldPrice = oldPrice;
        CostPrice = costPrice;
        Stock = stock;
        MinStock = minStock;
        ShowAvailability = showAvailability;
        FreeShipping = freeShipping;
        AdditionalShippingCharge = additionalShippingCharge;
        UnitOfMeasureId = unitOfMeasureId;
        QuantityPerUnitOfMeasure = quantityPerUnitOfMeasure;
        DeliveryWindowId = deliveryWindowId;
        MinOrderQuantity = minOrderQuantity;
        MaxOrderQuantity = maxOrderQuantity;
        IsFeatured = isFeatured;
        IsNew = isNew;
        IsBestSeller = isBestSeller;
        IsReturnable = isReturnable;
        Status = status;
        RedirectUrl = redirectUrl;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    public void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }
}