using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public class ProductAttributeValue : Entity, ICreatable, IUpdatable, IDeletable
{
    public int ProductAttributeId { get; private set; }
    public ProductAttribute? ProductAttribute { get; private set; }
    public string Value { get; protected set; }
    public string Slug { get; protected set; }
    public string Display { get; protected set; }
    public string Breadcrumb { get; protected set; }
    public decimal SortOrder { get; protected set; }
    public int CreatedBy { get; private set; }
    public IAppUser? CreatedByUser { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; protected set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public DateTime? UpdatedAt { get; protected set; }
    public string? UpdatedByIp { get; protected set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }

    private protected ProductAttributeValue()
    {
    }

    public ProductAttributeValue(string value, string slug, string display, string breadcrumb, decimal sortOrder,
        int createdBy, DateTime createdAt, string createdByIp)
    {
        ValidateValue(value);
        ValidateSlug(slug);
        ValidateDisplay(display);
        ValidateBreadcrumb(breadcrumb);
        ValidateCreatedBy(createdBy);
        ValidateCreatedByIp(createdByIp);

        Value = value;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    internal void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        ValidateDeletedBy(deletedBy);
        ValidateDeletedByIp(deletedByIp);

        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }

    internal bool Update(string value, string slug, string display, string breadcrumb, decimal sortOrder, int updatedBy,
        DateTime updatedAt, string updatedByIp)
    {
        ValidateValue(value);
        ValidateSlug(slug);
        ValidateDisplay(display);
        ValidateBreadcrumb(breadcrumb);
        ValidateUpdatedBy(updatedBy);
        ValidateUpdatedByIp(updatedByIp);

        if (Value == value && Slug == slug && Display == display && Breadcrumb == breadcrumb && SortOrder == sortOrder)
            return false;

        Value = value;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;

        return true;
    }

    private static void ValidateValue(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(DomainErrors.ProductAttributeValueErrors.ValueRequired);
        if (name.Length > 256) throw new DomainException(DomainErrors.ProductAttributeValueErrors.ValueTooLong);
    }

    private static void ValidateSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new DomainException(DomainErrors.ProductAttributeValueErrors.SlugRequired);
        if (slug.Length > 256) throw new DomainException(DomainErrors.ProductAttributeValueErrors.SlugTooLong);
    }

    private static void ValidateDisplay(string display)
    {
        if (string.IsNullOrWhiteSpace(display))
            throw new DomainException(DomainErrors.ProductAttributeValueErrors.DisplayRequired);
        if (display.Length > 256) throw new DomainException(DomainErrors.ProductAttributeValueErrors.DisplayTooLong);
    }

    private static void ValidateBreadcrumb(string breadcrumb)
    {
        if (string.IsNullOrWhiteSpace(breadcrumb))
            throw new DomainException(DomainErrors.ProductAttributeValueErrors.BreadcrumbRequired);
        if (breadcrumb.Length > 256)
            throw new DomainException(DomainErrors.ProductAttributeValueErrors.BreadcrumbTooLong);
    }

    private static void ValidateCreatedBy(int createdBy)
    {
        if (createdBy <= 0) throw new DomainException(DomainErrors.ProductAttributeValueErrors.InvalidCreatedBy);
    }

    private static void ValidateCreatedByIp(string createdByIp)
    {
        if (string.IsNullOrWhiteSpace(createdByIp))
            throw new DomainException(DomainErrors.ProductAttributeValueErrors.CreatedByIpRequired);
        if (createdByIp.Length > 128)
            throw new DomainException(DomainErrors.ProductAttributeValueErrors.CreatedByIpTooLong);
    }

    private static void ValidateUpdatedBy(int updatedBy)
    {
        if (updatedBy <= 0) throw new DomainException(DomainErrors.ProductAttributeValueErrors.InvalidUpdatedBy);
    }

    private static void ValidateUpdatedByIp(string updatedByIp)
    {
        if (string.IsNullOrWhiteSpace(updatedByIp))
            throw new DomainException(DomainErrors.ProductAttributeValueErrors.UpdatedByIpRequired);
        if (updatedByIp.Length > 128)
            throw new DomainException(DomainErrors.ProductAttributeValueErrors.UpdatedByIpTooLong);
    }

    private static void ValidateDeletedBy(int deletedBy)
    {
        if (deletedBy <= 0) throw new DomainException(DomainErrors.ProductAttributeValueErrors.InvalidDeletedBy);
    }

    private static void ValidateDeletedByIp(string deletedByIp)
    {
        if (string.IsNullOrWhiteSpace(deletedByIp))
            throw new DomainException(DomainErrors.ProductAttributeValueErrors.DeletedByIpRequired);
        if (deletedByIp.Length > 128)
            throw new DomainException(DomainErrors.ProductAttributeValueErrors.DeletedByIpTooLong);
    }
}