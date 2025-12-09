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
    public int? BrandId { get; private set; }
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
    public int Stock { get; private set; }
    public int MinStock { get; private set; }
    public bool ShowAvailability { get; private set; }
    public bool FreeShipping { get; private set; }
    public decimal AdditionalShippingCharge { get; private set; }
    public int UnitOfMeasureId { get; private set; }
    public UnitOfMeasure UnitOfMeasure { get; private set; }
    public decimal QuantityPerUnitOfMeasure { get; private set; }
    public int DeliveryWindowId { get; private set; }
    public DeliveryWindow DeliveryWindow { get; private set; }
    public int MinOrderQuantity { get; private set; }
    public int? MaxOrderQuantity { get; private set; }
    public bool IsFeatured { get; private set; }
    public bool IsNew { get; private set; }
    public bool IsBestSeller { get; private set; }
    public bool IsReturnable { get; private set; }
    public string? ReturnPolicy { get; private set; }
    public ProductStatus Status { get; private set; }
    public string? RedirectUrl { get; private set; }
    public int SortOrder { get; private set; }
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

    internal Product(string sku, string? gtin, string? mpn, string? mfc, string? ean, string? upc, string name,
        string slug, string display, string breadcrumb, string anchorText, string? anchorTitle, int? brandId,
        int? productGroupId, string? shortDescription, string? fullDescription, bool allowReviews, decimal price,
        decimal? oldPrice, decimal? costPrice, int stock, int minStock, bool showAvailability, bool freeShipping,
        decimal additionalShippingCharge, int unitOfMeasureId, decimal quantityPerUnitOfMeasure,
        int deliveryWindowId, int minOrderQuantity, int? maxOrderQuantity, bool isFeatured, bool isNew,
        bool isBestSeller, bool isReturnable, string? returnPolicy, ProductStatus status, string? redirectUrl,
        int sortOrder, int createdBy, DateTime createdAt, string createdByIp)
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
        ReturnPolicy = returnPolicy;
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

public class ProductBuilder
{
    private string _sku;
    private string? _gtin;
    private string? _mpn;
    private string? _mfc;
    private string? _ean;
    private string? _upc;
    private string _name;
    private string _slug;
    private string _display;
    private string _breadcrumb;
    private string _anchorText;
    private string? _anchorTitle;
    private int? _brandId;
    private int? _productGroupId;
    private string? _shortDescription;
    private string? _fullDescription;
    private bool _allowReviews;
    private decimal _price;
    private decimal? _oldPrice;
    private decimal? _costPrice;
    private int _stock;
    private int _minStock;
    private bool _showAvailability;
    private bool _freeShipping;
    private decimal _additionalShippingCharge;
    private decimal _weightKgs;
    private int _unitOfMeasureId;
    private decimal _quantityPerUnitOfMeasure;
    private int _deliveryWindowId;
    private int _minOrderQuantity;
    private int? _maxOrderQuantity;
    private bool _isFeatured;
    private bool _isNew;
    private bool _isBestSeller;
    private bool _isReturnable;
    private string? _returnPolicy;
    private ProductStatus _status;
    private string? _redirectUrl;
    private int _sortOrder;
    private int _createdBy;
    private DateTime _createdAt;
    private string _createdByIp;

    public ProductBuilder(string sku, string name, string slug, string display, string breadcrumb, string anchorText,
        int unitOfMeasureId, decimal quantityPerUnitOfMeasure, int deliveryWindowId, ProductStatus status,
        int createdBy, DateTime createdAt, string createdByIp)
    {
        _sku = sku;
        _name = name;
        _slug = slug;
        _display = display;
        _breadcrumb = breadcrumb;
        _anchorText = anchorText;
        _deliveryWindowId = deliveryWindowId;
        _unitOfMeasureId = unitOfMeasureId;
        _quantityPerUnitOfMeasure = quantityPerUnitOfMeasure;
        _status = status;
        _createdBy = createdBy;
        _createdAt = createdAt;
        _createdByIp = createdByIp;
    }

    public ProductBuilder HasGTIN(string? gtin)
    {
        _gtin = gtin;
        return this;
    }

    public ProductBuilder HasMPN(string? mpn)
    {
        _mpn = mpn;
        return this;
    }

    public ProductBuilder HasMFC(string? mfc)
    {
        _mfc = mfc;
        return this;
    }

    public ProductBuilder HasEAN(string? ean)
    {
        _ean = ean;
        return this;
    }

    public ProductBuilder HasUPC(string? upc)
    {
        _upc = upc;
        return this;
    }

    public ProductBuilder HasAnchorTitle(string? anchorTitle)
    {
        _anchorTitle = anchorTitle;
        return this;
    }

    public ProductBuilder HasBrandId(int? brandId)
    {
        _brandId = brandId;
        return this;
    }

    public ProductBuilder HasProductGroupId(int? productGroupId)
    {
        _productGroupId = productGroupId;
        return this;
    }

    public ProductBuilder HasFullDescription(string? fullDescription)
    {
        _fullDescription = fullDescription;
        return this;
    }

    public ProductBuilder HasShortDescription(string? shortDescription)
    {
        _shortDescription = shortDescription;
        return this;
    }

    public ProductBuilder AllowReviews(bool allowReviews)
    {
        _allowReviews = allowReviews;
        return this;
    }

    public ProductBuilder HasPrice(decimal price)
    {
        _price = price;
        return this;
    }

    public ProductBuilder HasOldPrice(decimal oldPrice)
    {
        _oldPrice = oldPrice;
        return this;
    }

    public ProductBuilder HasCostPrice(decimal costPrice)
    {
        _costPrice = costPrice;
        return this;
    }

    public ProductBuilder HasStock(int stock)
    {
        _stock = stock;
        return this;
    }

    public ProductBuilder HasMinStock(int minStock)
    {
        _minStock = minStock;
        return this;
    }

    public ProductBuilder ShowAvailability(bool showAvailability)
    {
        _showAvailability = showAvailability;
        return this;
    }

    public ProductBuilder IsFreeShipping(bool freeShipping)
    {
        _freeShipping = freeShipping;
        return this;
    }

    public ProductBuilder HasAdditionalShippingCharge(decimal additionalShippingCharge)
    {
        _additionalShippingCharge = additionalShippingCharge;
        return this;
    }

    public ProductBuilder HasWeightKgs(decimal weightKgs)
    {
        _weightKgs = weightKgs;
        return this;
    }

    public ProductBuilder HasMinOrderQuantity(int minOrderQuantity)
    {
        _minOrderQuantity = minOrderQuantity;
        return this;
    }

    public ProductBuilder HasMaxOrderQuantity(int maxOrderQuantity)
    {
        _maxOrderQuantity = maxOrderQuantity;
        return this;
    }

    public ProductBuilder IsFeatured(bool isFeatured)
    {
        _isFeatured = isFeatured;
        return this;
    }

    public ProductBuilder IsNew(bool isNew)
    {
        _isNew = isNew;
        return this;
    }

    public ProductBuilder IsBestSeller(bool isBestSeller)
    {
        _isBestSeller = isBestSeller;
        return this;
    }

    public ProductBuilder IsReturnable(bool isReturnable)
    {
        _isReturnable = isReturnable;
        return this;
    }

    public ProductBuilder HasReturnPolicy(string? returnPolicy)
    {
        _returnPolicy = returnPolicy;
        return this;
    }

    public ProductBuilder HasRedirectUrl(string? redirectUrl)
    {
        _redirectUrl = redirectUrl;
        return this;
    }

    public ProductBuilder HasSortOrder(int sortOrder)
    {
        _sortOrder = sortOrder;
        return this;
    }

    public Product Build()
    {
        return new Product(_sku, _gtin, _mpn, _mfc, _ean, _upc, _name, _slug, _display, _breadcrumb, _anchorText,
            _anchorTitle, _brandId, _productGroupId, _shortDescription, _fullDescription, _allowReviews, _price,
            _oldPrice, _costPrice, _stock, _minStock, _showAvailability, _freeShipping, _additionalShippingCharge,
            _unitOfMeasureId, _quantityPerUnitOfMeasure, _deliveryWindowId, _minOrderQuantity, _maxOrderQuantity,
            _isFeatured, _isNew, _isBestSeller, _isReturnable, _returnPolicy, _status, _redirectUrl, _sortOrder,
            _createdBy, _createdAt, _createdByIp);
    }
}