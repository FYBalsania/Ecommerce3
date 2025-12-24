using System.Collections.Immutable;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductGroup : EntityWithImages<ProductGroupImage>, ICreatable, IUpdatable, IDeletable
{
    public static readonly int NameMaxLength = 256;
    public static readonly int SlugMaxLength = 256;
    public static readonly int DisplayMaxLength = 256;
    public static readonly int BreadcrumbMaxLength = 256;
    public static readonly int AnchorTextMaxLength = 256;
    public static readonly int AnchorTitleMaxLength = 256;
    public static readonly int ShortDescriptionMaxLength = 512;

    private readonly List<ProductGroupProductAttribute> _attributes = [];
    public override string ImageNamePrefix => Slug;
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Display { get; set; }
    public string Breadcrumb { get; set; }
    public string AnchorText { get; private set; }
    public string? AnchorTitle { get; private set; }
    public string? ShortDescription { get; private set; }
    public string? FullDescription { get; private set; }
    public bool IsActive { get; private set; }
    public decimal SortOrder { get; private set; }
    public int CreatedBy { get; private set; }
    public IAppUser? CreatedByUser { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }
    public IReadOnlyList<ProductGroupProductAttribute> Attributes => _attributes;
    public ProductGroupPage? Page { get; private set; }

    private ProductGroup()
    {
    }

    public ProductGroup(string name, string slug, string display, string breadcrumb, string anchorText,
        string? anchorTitle, string? shortDescription, string? fullDescription, bool isActive, decimal sortOrder,
        int createdBy, string createdByIp)
    {
        ValidateRequiredAndTooLong(name, NameMaxLength, DomainErrors.ProductGroupErrors.NameRequired,
            DomainErrors.ProductGroupErrors.NameTooLong);
        ValidateRequiredAndTooLong(slug, SlugMaxLength, DomainErrors.ProductGroupErrors.SlugRequired,
            DomainErrors.ProductGroupErrors.SlugTooLong);
        ValidateRequiredAndTooLong(display, DisplayMaxLength, DomainErrors.ProductGroupErrors.DisplayRequired,
            DomainErrors.ProductGroupErrors.DisplayTooLong);
        ValidateRequiredAndTooLong(breadcrumb, BreadcrumbMaxLength, DomainErrors.ProductGroupErrors.BreadcrumbRequired,
            DomainErrors.ProductGroupErrors.BreadcrumbTooLong);
        ValidateRequiredAndTooLong(anchorText, AnchorTextMaxLength, DomainErrors.ProductGroupErrors.AnchorTextRequired,
            DomainErrors.ProductGroupErrors.AnchorTextTooLong);
        if (anchorTitle is not null)
            ValidateTooLong(anchorTitle, AnchorTitleMaxLength, DomainErrors.ProductGroupErrors.AnchorTitleTooLong);
        if (shortDescription is not null)
            ValidateTooLong(shortDescription, ShortDescriptionMaxLength,
                DomainErrors.ProductGroupErrors.ShortDescriptionTooLong);
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.ProductGroupErrors.InvalidCreatedBy);
        ICreatable.ValidateCreatedByIp(createdByIp, DomainErrors.ProductGroupErrors.CreatedByIpRequired,
            DomainErrors.ProductGroupErrors.CreatedByIpTooLong);

        Name = name;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        AnchorText = anchorText;
        AnchorTitle = anchorTitle;
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = isActive;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }

    public void Update(string name, string slug, string display, string breadcrumb, string anchorText,
        string? anchorTitle, string? shortDescription, string? fullDescription, bool isActive, decimal sortOrder,
        int updatedBy, string updatedByIp)
    {
        ValidateRequiredAndTooLong(name, NameMaxLength, DomainErrors.ProductGroupErrors.NameRequired,
            DomainErrors.ProductGroupErrors.NameTooLong);
        ValidateRequiredAndTooLong(slug, SlugMaxLength, DomainErrors.ProductGroupErrors.SlugRequired,
            DomainErrors.ProductGroupErrors.SlugTooLong);
        ValidateRequiredAndTooLong(display, DisplayMaxLength, DomainErrors.ProductGroupErrors.DisplayRequired,
            DomainErrors.ProductGroupErrors.DisplayTooLong);
        ValidateRequiredAndTooLong(breadcrumb, BreadcrumbMaxLength, DomainErrors.ProductGroupErrors.BreadcrumbRequired,
            DomainErrors.ProductGroupErrors.BreadcrumbTooLong);
        ValidateRequiredAndTooLong(anchorText, AnchorTextMaxLength, DomainErrors.ProductGroupErrors.AnchorTextRequired,
            DomainErrors.ProductGroupErrors.AnchorTextTooLong);
        if (anchorTitle is not null)
            ValidateTooLong(anchorTitle, AnchorTitleMaxLength, DomainErrors.ProductGroupErrors.AnchorTitleTooLong);
        if (shortDescription is not null)
            ValidateTooLong(shortDescription, ShortDescriptionMaxLength,
                DomainErrors.ProductGroupErrors.ShortDescriptionTooLong);
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.ProductGroupErrors.InvalidUpdatedBy);
        IUpdatable.ValidateUpdatedByIp(updatedByIp, DomainErrors.ProductGroupErrors.UpdatedByIpRequired,
            DomainErrors.ProductGroupErrors.UpdatedByIpTooLong);

        if (Name == name && Slug == slug && Display == display && Breadcrumb == breadcrumb &&
            AnchorText == anchorText && AnchorTitle == anchorTitle && ShortDescription == shortDescription &&
            FullDescription == fullDescription && IsActive == isActive && SortOrder == sortOrder)
            return;

        Name = name;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        AnchorText = anchorText;
        AnchorTitle = anchorTitle;
        ShortDescription = shortDescription;
        FullDescription = fullDescription;
        IsActive = isActive;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;
    }

    public void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        IDeletable.ValidateDeletedBy(deletedBy, DomainErrors.ProductGroupErrors.InvalidDeletedBy);
        IDeletable.ValidateDeletedByIp(deletedByIp, DomainErrors.ProductGroupErrors.DeletedByIpRequired,
            DomainErrors.ProductGroupErrors.DeletedByIpTooLong);

        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }

    public void AddAttribute(int productAttributeId, decimal productAttributeSortOrder,
        IDictionary<int, decimal> values, int createdBy, DateTime createdAt, string createdByIp)
    {
        //Validate inputs.
        ValidatePositiveNumber(productAttributeId,
            DomainErrors.ProductGroupProductAttributeErrors.InvalidProductAttributeId);
        if (values is null || values.Count == 0)
            throw new DomainException(DomainErrors.ProductGroupErrors.AttributeValueRequired);
        foreach (var keyValuePair in values)
        {
            ValidatePositiveNumber(keyValuePair.Key,
                DomainErrors.ProductGroupProductAttributeErrors.InvalidProductAttributeValueId);
        }

        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.ProductGroupProductAttributeErrors.InvalidCreatedBy);
        ICreatable.ValidateCreatedByIp(createdByIp, DomainErrors.ProductGroupProductAttributeErrors.CreatedByIpRequired,
            DomainErrors.ProductGroupProductAttributeErrors.CreatedByIpTooLong);

        //Check if attributeId already exists.
        if (_attributes.Any(x => x.ProductAttributeId == productAttributeId))
            throw new DomainException(DomainErrors.ProductGroupProductAttributeErrors.DuplicateProductAttributeId);

        //Add attribute values.
        foreach (var value in values)
        {
            _attributes.Add(new ProductGroupProductAttribute(productAttributeId, productAttributeSortOrder, value.Key,
                value.Value, createdBy, createdAt, createdByIp));
        }
    }

    public void UpdateAttribute(int productAttributeId, decimal productAttributeSortOrder,
        IDictionary<int, decimal> values, int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        //Validate inputs.
        ValidatePositiveNumber(productAttributeId,
            DomainErrors.ProductGroupProductAttributeErrors.InvalidProductAttributeId);
        if (values is null || values.Count == 0)
            throw new DomainException(DomainErrors.ProductGroupErrors.AttributeValueRequired);
        foreach (var keyValuePair in values)
        {
            ValidatePositiveNumber(keyValuePair.Key,
                DomainErrors.ProductGroupProductAttributeErrors.InvalidProductAttributeValueId);
        }

        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.ProductGroupProductAttributeErrors.InvalidUpdatedBy);
        IUpdatable.ValidateUpdatedByIp(updatedByIp, DomainErrors.ProductGroupProductAttributeErrors.UpdatedByIpRequired,
            DomainErrors.ProductGroupProductAttributeErrors.UpdatedByIpTooLong);

        //Check if attributeId exists.
        if (_attributes.All(x => x.ProductAttributeId != productAttributeId))
            throw new DomainException(DomainErrors.ProductGroupProductAttributeErrors.InvalidProductAttributeId);

        //Update Product Attribute SortOrder.
        _attributes.ForEach(x =>
            x.UpdateProductAttributeSortOrder(productAttributeSortOrder, updatedBy, updatedAt, updatedByIp));

        // 0. existing attribute's dictionary.
        var existing = _attributes
            .ToDictionary(x => x.ProductAttributeValueId, x => x.ProductAttributeValueSortOrder);

        // 1. Remove attribute's aren't present in desired (values parameter).
        var toRemove = existing.Keys
            .Where(key => !values.ContainsKey(key))
            .ToImmutableList(); // snapshot to avoid modifying during iteration.
        foreach (var attribute in toRemove
                     .Select(key => _attributes.First(x => x.ProductAttributeValueId == key)))
        {
            attribute.Delete(updatedBy, updatedAt, updatedByIp);
            _attributes.Remove(attribute);

            existing.Remove(attribute.ProductAttributeValueId);
        }

        // 2. Add new attributes and update changed values.
        foreach (var (key, value) in values)
        {
            if (existing.TryGetValue(key, out var existingValue))
            {
                if (existingValue != value)
                {
                    var attribute = _attributes.First(x => x.ProductAttributeValueId == key);
                    attribute.UpdateProductAttributeValueSortOrder(value, updatedBy, updatedAt, updatedByIp);
                    existing[key] = value; // update
                }
            }
            else
            {
                _attributes.Add(new ProductGroupProductAttribute(productAttributeId, productAttributeSortOrder, key,
                    value, updatedBy, updatedAt, updatedByIp));
                existing.Add(key, value); // add
            }
        }
    }
}