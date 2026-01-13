using System.Net;
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
    public IPAddress CreatedByIp { get; private set; }
    public int? UpdatedBy { get; protected set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public DateTime? UpdatedAt { get; protected set; }
    public IPAddress? UpdatedByIp { get; protected set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public IPAddress? DeletedByIp { get; private set; }

    private protected ProductAttributeValue()
    {
    }

    public ProductAttributeValue(string value, string slug, string display, string breadcrumb, decimal sortOrder,
        int createdBy, DateTime createdAt, IPAddress createdByIp)
    {
        ValidateValue(value);
        ValidateSlug(slug);
        ValidateDisplay(display);
        ValidateBreadcrumb(breadcrumb);
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.ProductAttributeValueErrors.InvalidCreatedBy);
        
        Value = value;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    internal void Delete(int deletedBy, DateTime deletedAt, IPAddress deletedByIp)
    {
        IDeletable.ValidateDeletedBy(deletedBy, DomainErrors.ProductAttributeValueErrors.InvalidDeletedBy);
        
        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }

    internal bool Update(string value, string slug, string display, string breadcrumb, decimal sortOrder, int updatedBy,
        DateTime updatedAt, IPAddress updatedByIp)
    {
        ValidateValue(value);
        ValidateSlug(slug);
        ValidateDisplay(display);
        ValidateBreadcrumb(breadcrumb);
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.ProductAttributeValueErrors.InvalidUpdatedBy);
        
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
}