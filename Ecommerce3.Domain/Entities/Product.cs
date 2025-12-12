using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class Product : EntityWithImages<ProductImage>, ICreatable, IUpdatable, IDeletable,
    IKVPListItems<ProductKVPListItem>
{
    public static readonly int SKUMaxLength = 16;
    public static readonly int GTINMaxLength = 16;
    public static readonly int MPNMaxLength = 64;
    public static readonly int MFCMaxLength = 64;
    public static readonly int EANMaxLength = 64;
    public static readonly int UPCMaxLength = 64;
    public static readonly int NameMaxLength = 256;
    public static readonly int SlugMaxLength = 256;
    public static readonly int DisplayMaxLength = 256;
    public static readonly int BreadcrumbMaxLength = 256;
    public static readonly int AnchorTextMaxLength = 256;
    public static readonly int AnchorTitleMaxLength = 256;
    public static readonly int ShortDescriptionMaxLength = 1024;
    public static readonly int RedirectUrlMaxLength = 2048;

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
    public decimal MinOrderQuantity { get; private set; }
    public decimal? MaxOrderQuantity { get; private set; }
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
        decimal? oldPrice, decimal? costPrice, decimal stock, decimal? minStock, bool showAvailability,
        bool freeShipping, decimal additionalShippingCharge, int unitOfMeasureId, decimal quantityPerUnitOfMeasure,
        int deliveryWindowId, decimal minOrderQuantity, decimal? maxOrderQuantity, bool isFeatured, bool isNew,
        bool isBestSeller, bool isReturnable, ProductStatus status, string? redirectUrl,
        decimal sortOrder, int createdBy, DateTime createdAt, string createdByIp)
    {
        ValidateRequiredAndTooLong(sku, SKUMaxLength, DomainErrors.ProductErrors.SKURequired,
            DomainErrors.ProductErrors.SKUTooLong);
        if (gtin is not null) ValidateTooLong(gtin, GTINMaxLength, DomainErrors.ProductErrors.GTINTooLong);
        if (mpn is not null) ValidateTooLong(mpn, MPNMaxLength, DomainErrors.ProductErrors.MPNTooLong);
        if (mfc is not null) ValidateTooLong(mfc, MFCMaxLength, DomainErrors.ProductErrors.MFCTooLong);
        if (ean is not null) ValidateTooLong(ean, EANMaxLength, DomainErrors.ProductErrors.EANTooLong);
        if (upc is not null) ValidateTooLong(upc, UPCMaxLength, DomainErrors.ProductErrors.UPCTooLong);
        ValidateRequiredAndTooLong(name, NameMaxLength, DomainErrors.ProductErrors.NameRequired,
            DomainErrors.ProductErrors.NameTooLong);

        ValidateRequiredAndTooLong(slug, SlugMaxLength, DomainErrors.ProductErrors.SlugRequired,
            DomainErrors.ProductErrors.SlugTooLong);

        ValidateRequiredAndTooLong(display, DisplayMaxLength, DomainErrors.ProductErrors.DisplayRequired,
            DomainErrors.ProductErrors.DisplayTooLong);

        ValidateRequiredAndTooLong(breadcrumb, BreadcrumbMaxLength, DomainErrors.ProductErrors.BreadcrumbRequired,
            DomainErrors.ProductErrors.BreadcrumbTooLong);

        ValidateRequiredAndTooLong(anchorText, AnchorTextMaxLength, DomainErrors.ProductErrors.AnchorTextRequired,
            DomainErrors.ProductErrors.AnchorTextTooLong);

        if (anchorTitle is not null)
            ValidateTooLong(anchorTitle, AnchorTitleMaxLength, DomainErrors.ProductErrors.AnchorTitleTooLong);

        ValidatePositiveNumber(brandId, DomainErrors.ProductErrors.InvalidBrandId);

        if (productGroupId is not null)
            ValidatePositiveNumber((int)productGroupId, DomainErrors.ProductErrors.InvalidProductGroupId);

        if (shortDescription is not null)
            ValidateTooLong(shortDescription, ShortDescriptionMaxLength,
                DomainErrors.ProductErrors.ShortDescriptionTooLong);
        ValidatePositiveNumber(price, DomainErrors.ProductErrors.InvalidPrice);
        if (oldPrice is not null) ValidatePositiveNumber(oldPrice.Value, DomainErrors.ProductErrors.InvalidOldPrice);
        if (costPrice is not null) ValidatePositiveNumber(costPrice.Value, DomainErrors.ProductErrors.InvalidCostPrice);
        ValidatePositiveNumber(stock, DomainErrors.ProductErrors.InvalidStock);
        if (minStock is not null) ValidatePositiveNumber(minStock.Value, DomainErrors.ProductErrors.InvalidMinStock);
        ValidatePositiveAndZeroNumber(additionalShippingCharge,
            DomainErrors.ProductErrors.InvalidAdditionalShippingCharge);
        ValidatePositiveNumber(unitOfMeasureId, DomainErrors.ProductErrors.InvalidUnitOfMeasureId);
        ValidatePositiveNumber(quantityPerUnitOfMeasure, DomainErrors.ProductErrors.InvalidQuantityPerUnitOfMeasure);
        ValidatePositiveNumber(deliveryWindowId, DomainErrors.ProductErrors.InvalidDeliveryWindowId);
        ValidateMinMaxOrderQuantity(minOrderQuantity, maxOrderQuantity);
        ValidateProductStatusAndRedirectUrl(status, redirectUrl);
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.ProductErrors.InvalidCreatedBy);
        ICreatable.ValidateCreatedByIp(createdByIp, DomainErrors.ProductErrors.CreatedByIpRequired,
            DomainErrors.ProductErrors.CreatedByIpTooLong);

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

    private static void ValidateMinMaxOrderQuantity(decimal minOrderQuantity, decimal? maxOrderQuantity)
    {
        ValidatePositiveNumber(minOrderQuantity, DomainErrors.ProductErrors.InvalidMinOrderQuantity);
        if (maxOrderQuantity is null) return;
        ValidatePositiveNumber(maxOrderQuantity.Value, DomainErrors.ProductErrors.InvalidMaxOrderQuantity);
        if (maxOrderQuantity < minOrderQuantity)
            throw new DomainException(DomainErrors.ProductErrors.InvalidMaxOrderQuantity);
    }

    private static void ValidateProductStatusAndRedirectUrl(ProductStatus status, string? redirectUrl)
    {
        if (status == ProductStatus.RedirectToUrl)
        {
            ValidateRequiredAndTooLong(redirectUrl, RedirectUrlMaxLength,
                DomainErrors.ProductErrors.RedirectUrlRequired, DomainErrors.ProductErrors.RedirectUrlTooLong);
            ValidateUrl(redirectUrl, UriKind.RelativeOrAbsolute, DomainErrors.ProductErrors.InvalidRedirectUrl);
        }
        else
        {
            if (redirectUrl is not null)
                throw new DomainException(DomainErrors.ProductErrors.RedirectUrlNotRequired);
        }
    }
}