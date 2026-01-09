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
    public decimal AverageRating { get; private set; }
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
    public int CountryOfOriginId { get; private set; }
    public Country? CountryOfOrigin { get; private set; }
    public List<string> Facets { get; private set; } = [];
    public int CreatedBy { get; }
    public IAppUser? CreatedByUser { get; }
    public DateTime CreatedAt { get; }
    public string CreatedByIp { get; }
    public int? UpdatedBy { get; private set; }
    public IAppUser? UpdatedByUser { get; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
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
    public ProductPage? Page { get; private set; }

    private Product()
    {
    }

    public Product(string sku, string? gtin, string? mpn, string? mfc, string? ean, string? upc, string name,
        string slug, string display, string breadcrumb, string anchorText, string? anchorTitle,
        KeyValuePair<int, string> brand,
        IDictionary<int, string> categories,
        KeyValuePair<int, string>? productGroup,
        IReadOnlyCollection<ValueObjects.ProductAttribute> attributes,
        string? shortDescription, string? fullDescription, bool allowReviews, decimal price, decimal? oldPrice,
        decimal? costPrice, decimal stock, decimal? minStock, bool showAvailability, bool freeShipping,
        decimal additionalShippingCharge, int unitOfMeasureId, decimal quantityPerUnitOfMeasure, int deliveryWindowId,
        decimal minOrderQuantity, decimal? maxOrderQuantity, bool isFeatured, bool isNew, bool isBestSeller,
        bool isReturnable, ProductStatus status, string? redirectUrl, int countryOfOriginId, decimal sortOrder,
        string? h1, string metaTitle,
        string? metaDescription, string? metaKeywords, int createdBy, DateTime createdAt, string createdByIp)
    {
        //SKU.
        ValidateRequiredAndTooLong(sku, SKUMaxLength, DomainErrors.ProductErrors.SKURequired,
            DomainErrors.ProductErrors.SKUTooLong);
        //GTIN.
        if (gtin is not null) ValidateTooLong(gtin, GTINMaxLength, DomainErrors.ProductErrors.GTINTooLong);
        //MPN.
        if (mpn is not null) ValidateTooLong(mpn, MPNMaxLength, DomainErrors.ProductErrors.MPNTooLong);
        //MFC.
        if (mfc is not null) ValidateTooLong(mfc, MFCMaxLength, DomainErrors.ProductErrors.MFCTooLong);
        //EAN.
        if (ean is not null) ValidateTooLong(ean, EANMaxLength, DomainErrors.ProductErrors.EANTooLong);
        //UPC.
        if (upc is not null) ValidateTooLong(upc, UPCMaxLength, DomainErrors.ProductErrors.UPCTooLong);
        //Name.
        ValidateRequiredAndTooLong(name, NameMaxLength, DomainErrors.ProductErrors.NameRequired,
            DomainErrors.ProductErrors.NameTooLong);
        //Slug.
        ValidateRequiredAndTooLong(slug, SlugMaxLength, DomainErrors.ProductErrors.SlugRequired,
            DomainErrors.ProductErrors.SlugTooLong);
        //Display.
        ValidateRequiredAndTooLong(display, DisplayMaxLength, DomainErrors.ProductErrors.DisplayRequired,
            DomainErrors.ProductErrors.DisplayTooLong);
        //Breadcrumb.
        ValidateRequiredAndTooLong(breadcrumb, BreadcrumbMaxLength, DomainErrors.ProductErrors.BreadcrumbRequired,
            DomainErrors.ProductErrors.BreadcrumbTooLong);
        //AnchorText.
        ValidateRequiredAndTooLong(anchorText, AnchorTextMaxLength, DomainErrors.ProductErrors.AnchorTextRequired,
            DomainErrors.ProductErrors.AnchorTextTooLong);
        //AnchorTitle.
        if (anchorTitle is not null)
            ValidateTooLong(anchorTitle, AnchorTitleMaxLength, DomainErrors.ProductErrors.AnchorTitleTooLong);
        //BrandId.
        ValidatePositiveNumber(brand.Key, DomainErrors.ProductErrors.InvalidBrandId);
        //CategoryIds.
        ValidateCategoryIds(categories.Select(x => x.Key).ToArray());
        //ProductGroupId.
        if (productGroup is not null)
        {
            ValidatePositiveNumber(productGroup.Value.Key, DomainErrors.ProductErrors.InvalidProductGroupId);
            foreach (var attribute in attributes)
            {
                ValidatePositiveNumber(attribute.ProductAttributeId,
                    DomainErrors.ProductAttributeErrors.InvalidProductAttributeId);
                ValidatePositiveNumber(attribute.ProductAttributeValueId,
                    DomainErrors.ProductAttributeValueErrors.InvalidId);
            }
        }

        //ShortDescription.
        if (shortDescription is not null)
            ValidateTooLong(shortDescription, ShortDescriptionMaxLength,
                DomainErrors.ProductErrors.ShortDescriptionTooLong);
        //Price.
        ValidatePositiveNumber(price, DomainErrors.ProductErrors.InvalidPrice);
        //OldPrice.
        if (oldPrice is not null) ValidatePositiveNumber(oldPrice.Value, DomainErrors.ProductErrors.InvalidOldPrice);
        //CostPrice.
        if (costPrice is not null) ValidatePositiveNumber(costPrice.Value, DomainErrors.ProductErrors.InvalidCostPrice);
        //Stock.
        ValidatePositiveNumber(stock, DomainErrors.ProductErrors.InvalidStock);
        //MinStock.
        if (minStock is not null) ValidatePositiveNumber(minStock.Value, DomainErrors.ProductErrors.InvalidMinStock);
        //AdditionalShippingCharge.
        ValidatePositiveAndZeroNumber(additionalShippingCharge,
            DomainErrors.ProductErrors.InvalidAdditionalShippingCharge);
        //UnitOfMeasureId.
        ValidatePositiveNumber(unitOfMeasureId, DomainErrors.ProductErrors.InvalidUnitOfMeasureId);
        //QuantityPerUnitOfMeasure.
        ValidatePositiveNumber(quantityPerUnitOfMeasure, DomainErrors.ProductErrors.InvalidQuantityPerUnitOfMeasure);
        //DeliveryWindowId.
        ValidatePositiveNumber(deliveryWindowId, DomainErrors.ProductErrors.InvalidDeliveryWindowId);
        //MinOrderQuantity & MaxOrderQuantity.
        ValidateMinMaxOrderQuantity(minOrderQuantity, maxOrderQuantity);
        //CountryOfOrigin.
        ValidatePositiveNumber(countryOfOriginId, DomainErrors.ProductErrors.InvalidCountryOfOriginId);
        //Status & RedirectUrl.
        ValidateProductStatusAndRedirectUrl(status, redirectUrl);
        //CreatedBy.
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.ProductErrors.InvalidCreatedBy);
        //CreatedByIp.
        ICreatable.ValidateCreatedByIp(createdByIp, DomainErrors.ProductErrors.CreatedByIpRequired,
            DomainErrors.ProductErrors.CreatedByIpTooLong);
        //Validate H1.
        if (h1 is not null) ValidateTooLong(h1, 256, DomainErrors.PageErrors.H1TooLong);
        //Validate MetaTitle.
        ValidateRequiredAndTooLong(metaTitle, 256, DomainErrors.PageErrors.MetaTitleRequired,
            DomainErrors.PageErrors.MetaTitleTooLong);
        if (metaDescription is not null)
            ValidateTooLong(metaDescription, 2048, DomainErrors.PageErrors.MetaDescriptionTooLong);
        if (metaKeywords is not null) ValidateTooLong(metaKeywords, 1024, DomainErrors.PageErrors.MetaKeywordsTooLong);

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
        BrandId = brand.Key;
        _categories.AddRange(categories.Select((category, index) =>
            new ProductCategory(category.Key, index == 0, index, createdBy, createdAt, createdByIp)));
        ProductGroupId = productGroup?.Key;
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
        CountryOfOriginId = countryOfOriginId;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;

        //Attributes.
        if (productGroup is not null)
            AddAttributes(attributes, createdBy, createdAt, createdByIp);

        //Page.
        Page = new ProductPage(null, metaTitle, metaDescription, metaKeywords, null,
            h1, null, null, null, null, null, null, null,
            null, null, null, null, 0, SiteMapFrequency.Always,
            null, true, null, null, "en", "IN", 0,
            true, createdBy, createdAt, createdByIp);

        //Facets.
        SetFacets(false, categories, productGroup, attributes);
    }

    private void SetFacets(bool clear, IDictionary<int, string> categories, KeyValuePair<int, string>? productGroup
        , IReadOnlyCollection<ValueObjects.ProductAttribute> attributes)
    {
        if (clear) Facets.Clear();
        foreach (var category in categories)
        {
            Facets.Add($"category:{category.Key}");
        }

        if (productGroup is not null)
        {
            foreach (var attribute in attributes)
            {
                Facets.Add($"attribute:{attribute.ProductAttributeId}:{attribute.ProductAttributeValueId}");
            }
        }
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

    private static void ValidateCategoryIds(int[] categoryIds)
    {
        if (categoryIds is null || categoryIds.Length == 0)
            throw new DomainException(DomainErrors.ProductErrors.CategoryIdRequired);
        if (categoryIds.Length != categoryIds.Distinct().Count())
            throw new DomainException(DomainErrors.ProductErrors.DuplicateCategoryId);

        foreach (var categoryId in categoryIds)
        {
            ValidatePositiveNumber(categoryId, DomainErrors.ProductErrors.InvalidCategoryId);
        }
    }

    public void Update(string sku, string? gtin, string? mpn, string? mfc, string? ean, string? upc, string name,
        string slug, string display, string breadcrumb, string anchorText, string? anchorTitle,
        KeyValuePair<int, string> brand,
        IDictionary<int, string> categories,
        KeyValuePair<int, string>? productGroup,
        IReadOnlyCollection<ValueObjects.ProductAttribute> attributes,
        string? shortDescription, string? fullDescription, bool allowReviews,
        decimal price, decimal? oldPrice, decimal? costPrice, decimal stock, decimal? minStock, bool showAvailability,
        bool freeShipping, decimal additionalShippingCharge, int unitOfMeasureId, decimal quantityPerUnitOfMeasure,
        int deliveryWindowId, decimal minOrderQuantity, decimal? maxOrderQuantity, bool isFeatured, bool isNew,
        bool isBestSeller, bool isReturnable, ProductStatus status, string? redirectUrl, int countryOfOriginId,
        decimal sortOrder, string? h1, string metaTitle, string? metaDescription, string? metaKeywords,
        int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        //SKU.
        ValidateRequiredAndTooLong(sku, SKUMaxLength, DomainErrors.ProductErrors.SKURequired,
            DomainErrors.ProductErrors.SKUTooLong);
        //GTIN.
        if (gtin is not null) ValidateTooLong(gtin, GTINMaxLength, DomainErrors.ProductErrors.GTINTooLong);
        //MPN.
        if (mpn is not null) ValidateTooLong(mpn, MPNMaxLength, DomainErrors.ProductErrors.MPNTooLong);
        //MFC.
        if (mfc is not null) ValidateTooLong(mfc, MFCMaxLength, DomainErrors.ProductErrors.MFCTooLong);
        //EAN.
        if (ean is not null) ValidateTooLong(ean, EANMaxLength, DomainErrors.ProductErrors.EANTooLong);
        //UPC.
        if (upc is not null) ValidateTooLong(upc, UPCMaxLength, DomainErrors.ProductErrors.UPCTooLong);
        //Name.
        ValidateRequiredAndTooLong(name, NameMaxLength, DomainErrors.ProductErrors.NameRequired,
            DomainErrors.ProductErrors.NameTooLong);
        //Slug.
        ValidateRequiredAndTooLong(slug, SlugMaxLength, DomainErrors.ProductErrors.SlugRequired,
            DomainErrors.ProductErrors.SlugTooLong);
        //Display.
        ValidateRequiredAndTooLong(display, DisplayMaxLength, DomainErrors.ProductErrors.DisplayRequired,
            DomainErrors.ProductErrors.DisplayTooLong);
        //Breadcrumb.
        ValidateRequiredAndTooLong(breadcrumb, BreadcrumbMaxLength, DomainErrors.ProductErrors.BreadcrumbRequired,
            DomainErrors.ProductErrors.BreadcrumbTooLong);
        //AnchorText.
        ValidateRequiredAndTooLong(anchorText, AnchorTextMaxLength, DomainErrors.ProductErrors.AnchorTextRequired,
            DomainErrors.ProductErrors.AnchorTextTooLong);
        //AnchorTitle.
        if (anchorTitle is not null)
            ValidateTooLong(anchorTitle, AnchorTitleMaxLength, DomainErrors.ProductErrors.AnchorTitleTooLong);
        //BrandId.
        ValidatePositiveNumber(brand.Key, DomainErrors.ProductErrors.InvalidBrandId);
        //CategoryIds.
        ValidateCategoryIds(categories.Select(x => x.Key).ToArray());
        //ProductGroupId.
        if (productGroup is not null)
            ValidatePositiveNumber(productGroup.Value.Key, DomainErrors.ProductErrors.InvalidProductGroupId);
        //ShortDescription.
        if (shortDescription is not null)
            ValidateTooLong(shortDescription, ShortDescriptionMaxLength,
                DomainErrors.ProductErrors.ShortDescriptionTooLong);
        //Price.
        ValidatePositiveNumber(price, DomainErrors.ProductErrors.InvalidPrice);
        //OldPrice.
        if (oldPrice is not null) ValidatePositiveNumber(oldPrice.Value, DomainErrors.ProductErrors.InvalidOldPrice);
        //CostPrice.
        if (costPrice is not null) ValidatePositiveNumber(costPrice.Value, DomainErrors.ProductErrors.InvalidCostPrice);
        //Stock.
        ValidatePositiveNumber(stock, DomainErrors.ProductErrors.InvalidStock);
        //MinStock.
        if (minStock is not null) ValidatePositiveNumber(minStock.Value, DomainErrors.ProductErrors.InvalidMinStock);
        //AdditionalShippingCharge.
        ValidatePositiveAndZeroNumber(additionalShippingCharge,
            DomainErrors.ProductErrors.InvalidAdditionalShippingCharge);
        //UnitOfMeasureId.
        ValidatePositiveNumber(unitOfMeasureId, DomainErrors.ProductErrors.InvalidUnitOfMeasureId);
        //QuantityPerUnitOfMeasure.
        ValidatePositiveNumber(quantityPerUnitOfMeasure, DomainErrors.ProductErrors.InvalidQuantityPerUnitOfMeasure);
        //DeliveryWindowId.
        ValidatePositiveNumber(deliveryWindowId, DomainErrors.ProductErrors.InvalidDeliveryWindowId);
        //MinOrderQuantity & MaxOrderQuantity.
        ValidateMinMaxOrderQuantity(minOrderQuantity, maxOrderQuantity);
        //Status & RedirectUrl.
        ValidateProductStatusAndRedirectUrl(status, redirectUrl);
        //CountryOfOriginId.
        ValidatePositiveNumber(countryOfOriginId, DomainErrors.ProductErrors.InvalidCountryOfOriginId);
        //CreatedBy.
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.ProductErrors.InvalidCreatedBy);
        //CreatedByIp.
        IUpdatable.ValidateUpdatedByIp(updatedByIp, DomainErrors.ProductErrors.CreatedByIpRequired,
            DomainErrors.ProductErrors.CreatedByIpTooLong);
        //Validate H1.
        if (h1 is not null) ValidateTooLong(h1, 256, DomainErrors.PageErrors.H1TooLong);
        //Validate MetaTitle.
        ValidateRequiredAndTooLong(metaTitle, 256, DomainErrors.PageErrors.MetaTitleRequired,
            DomainErrors.PageErrors.MetaTitleTooLong);
        if (metaDescription is not null)
            ValidateTooLong(metaDescription, 2048, DomainErrors.PageErrors.MetaDescriptionTooLong);
        if (metaKeywords is not null) ValidateTooLong(metaKeywords, 1024, DomainErrors.PageErrors.MetaKeywordsTooLong);

        if (SKU == sku && GTIN == gtin && MPN == mpn && MFC == mfc && EAN == ean && UPC == upc && Name == name &&
            Slug == slug && Display == display && Breadcrumb == breadcrumb && AnchorText == anchorText &&
            AnchorTitle == anchorTitle &&
            BrandId == brand.Key &&
            categories.Select(x => x.Key)
                .SequenceEqual(_categories.OrderByDescending(x => x.IsPrimary).Select(x => x.CategoryId)) &&
            ProductGroupId == productGroup?.Key &&
            ShortDescription == shortDescription &&
            AreDictionariesEqualInOrder(
                attributes.ToDictionary(x => x.ProductAttributeId, x => x.ProductAttributeValueId),
                _attributes.ToDictionary(x => x.ProductAttributeId, x => x.ProductAttributeValueId)) &&
            FullDescription == fullDescription && AllowReviews == allowReviews && Price == price &&
            OldPrice == oldPrice && CostPrice == costPrice && Stock == stock && MinStock == minStock &&
            ShowAvailability == showAvailability && FreeShipping == freeShipping &&
            AdditionalShippingCharge == additionalShippingCharge && UnitOfMeasureId == unitOfMeasureId &&
            QuantityPerUnitOfMeasure == quantityPerUnitOfMeasure && DeliveryWindowId == deliveryWindowId &&
            MinOrderQuantity == minOrderQuantity && MaxOrderQuantity == maxOrderQuantity && IsFeatured == isFeatured &&
            IsNew == isNew && IsBestSeller == isBestSeller && IsReturnable == isReturnable && Status == status &&
            RedirectUrl == redirectUrl && CountryOfOriginId == countryOfOriginId && SortOrder == sortOrder
           )
            return;

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
        BrandId = brand.Key;
        UpdateCategories(categories.Select(x => x.Key).ToArray(), updatedBy, updatedAt, updatedByIp);

        //ProductGroup & attributes.
        if (ProductGroupId is null && productGroup is not null)
            AddAttributes(attributes, updatedBy, updatedAt, updatedByIp);
        else if (ProductGroupId is not null && productGroup is null)
            DeleteAttributes(updatedBy, updatedAt, updatedByIp);
        else if (ProductGroupId is not null && productGroup is not null)
        {
            if (ProductGroupId != productGroup.Value.Key)
            {
                DeleteAttributes(updatedBy, updatedAt, updatedByIp);
                AddAttributes(attributes, updatedBy, updatedAt, updatedByIp);
            }
            else
            {
                foreach (var attribute in attributes)
                {
                    var existingAttribute =
                        _attributes.FirstOrDefault(x => x.ProductAttributeId == attribute.ProductAttributeId);
                    if (existingAttribute is null)
                        //When a new attribute was added to the product group after this product was created.
                        _attributes.Add(new ProductProductAttribute(attribute.ProductAttributeId,
                            attribute.ProductAttributeSortOrder, attribute.ProductAttributeValueId,
                            attribute.ProductAttributeValueSortOrder, updatedBy, updatedAt, updatedByIp));
                    else
                        existingAttribute.UpdateValueId(attribute.ProductAttributeValueId, updatedBy, updatedAt,
                            updatedByIp);
                }
            }
        }

        ProductGroupId = productGroup?.Key;
        //ProductGroup & attributes.

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
        CountryOfOriginId = countryOfOriginId;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;

        //Facets
        SetFacets(true, categories, productGroup, attributes);
    }

    private void UpdateCategories(int[] categoryIds, int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        var desired = categoryIds
            .Select((categoryId, index) => new
            {
                CategoryId = categoryId,
                SortOrder = index,
                IsPrimary = index == 0
            })
            .ToList();

        var existingByCategoryId = _categories
            .ToDictionary(x => x.CategoryId);

        // REMOVE categories no longer present
        _categories.RemoveAll(pc =>
        {
            if (desired.Any(d => d.CategoryId == pc.CategoryId))
                return false;

            pc.Delete(updatedBy, updatedAt, updatedByIp);
            return true;
        });

        // ADD or UPDATE
        foreach (var d in desired)
        {
            if (existingByCategoryId.TryGetValue(d.CategoryId, out var existing))
            {
                existing.Update(d.IsPrimary, d.SortOrder, updatedBy, updatedAt, updatedByIp);
            }
            else
            {
                _categories.Add(new ProductCategory(d.CategoryId, d.IsPrimary, d.SortOrder, updatedBy, updatedAt,
                    updatedByIp));
            }
        }
    }

    private bool AreDictionariesEqualInOrder(IDictionary<int, int> dict1, IDictionary<int, int> dict2)
    {
        if (ReferenceEquals(dict1, dict2))
            return true;

        if (dict1 is null || dict2 == null)
            return false;

        if (dict1.Count != dict2.Count)
            return false;

        using var enum1 = dict1.GetEnumerator();
        using var enum2 = dict2.GetEnumerator();

        while (enum1.MoveNext() && enum2.MoveNext())
        {
            if (enum1.Current.Key != enum2.Current.Key ||
                enum1.Current.Value != enum2.Current.Value)
                return false;
        }

        return true;
    }

    private void AddAttributes(IReadOnlyCollection<ValueObjects.ProductAttribute> attributes, int createdBy,
        DateTime createdAt, string createdByIp)
    {
        foreach (var attribute in attributes)
        {
            _attributes.Add(
                new ProductProductAttribute(attribute.ProductAttributeId, attribute.ProductAttributeSortOrder,
                    attribute.ProductAttributeValueId, attribute.ProductAttributeValueSortOrder, createdBy,
                    createdAt, createdByIp));
        }
    }

    private void DeleteAttributes(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        foreach (var attribute in _attributes.ToList())
        {
            attribute.Delete(deletedBy, deletedAt, deletedByIp);
            _attributes.Remove(attribute);
        }
    }

    public void Update(decimal price, decimal? oldPrice, decimal stock, int updatedBy, DateTime updatedAt,
        string updatedByIp)
    {
        ValidatePositiveNumber(price, DomainErrors.ProductErrors.InvalidPrice);
        if (oldPrice is not null) ValidatePositiveNumber(oldPrice.Value, DomainErrors.ProductErrors.InvalidOldPrice);
        ValidatePositiveNumber(stock, DomainErrors.ProductErrors.InvalidStock);
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.ProductErrors.InvalidCreatedBy);
        IUpdatable.ValidateUpdatedByIp(updatedByIp, DomainErrors.ProductErrors.CreatedByIpRequired,
            DomainErrors.ProductErrors.CreatedByIpTooLong);

        if (Price == price && OldPrice == oldPrice && Stock == stock)
            return;

        Price = price;
        OldPrice = oldPrice;
        Stock = stock;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;
    }
}