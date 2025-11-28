using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductAttributeDecimalValue : ProductAttributeValue
{
    public decimal DecimalValue { get; private set; }

    public ProductAttributeDecimalValue(string value, string slug, string display, string breadcrumb,
        decimal decimalValue, int sortOrder, int createdBy, DateTime createdAt, string createdByIp)
        : base(value, slug, display, breadcrumb, sortOrder, createdBy, createdAt, createdByIp)
    {
        DecimalValue = decimalValue;
    }
    
    public bool Update(string value, string slug, string display, string breadcrumb, int sortOrder, 
        decimal decimalValue, int updatedBy, string updatedByIp)
    {
        ValidateName(value);
        ValidateSlug(slug);
        ValidateDisplay(display);
        ValidateBreadcrumb(breadcrumb);
        ValidateUpdatedBy(updatedBy);
        ValidateUpdatedByIp(updatedByIp);

        if (Value == value && Slug == slug && Display == display && Breadcrumb == breadcrumb && SortOrder == sortOrder &&
            DecimalValue == decimalValue)
            return false;

        Value = value;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        SortOrder = sortOrder; 
        DecimalValue = decimalValue;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;

        return true;
    }
    
    private static void ValidateUpdatedByIp(string updatedByIp)
    {
        if (string.IsNullOrWhiteSpace(updatedByIp)) throw new DomainException(DomainErrors.ProductAttributeValueErrors.UpdatedByIpRequired);
        if (updatedByIp.Length > 128) throw new DomainException(DomainErrors.ProductAttributeValueErrors.UpdatedByIpTooLong);
    }
    
    private static void ValidateUpdatedBy(int updatedBy)
    {
        if (updatedBy <= 0) throw new DomainException(DomainErrors.ProductAttributeValueErrors.InvalidUpdatedBy);
    }
    
    private static void ValidateBreadcrumb(string breadcrumb)
    {
        if (string.IsNullOrWhiteSpace(breadcrumb))
            throw new DomainException(DomainErrors.ProductAttributeValueErrors.BreadcrumbRequired);
        if (breadcrumb.Length > 256) throw new DomainException(DomainErrors.ProductAttributeValueErrors.BreadcrumbTooLong);
    }

    private static void ValidateDisplay(string display)
    {
        if (string.IsNullOrWhiteSpace(display)) throw new DomainException(DomainErrors.ProductAttributeValueErrors.DisplayRequired);
        if (display.Length > 256) throw new DomainException(DomainErrors.ProductAttributeValueErrors.DisplayTooLong);
    }

    private static void ValidateSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug)) throw new DomainException(DomainErrors.ProductAttributeValueErrors.SlugRequired);
        if (slug.Length > 256) throw new DomainException(DomainErrors.ProductAttributeValueErrors.SlugTooLong);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new DomainException(DomainErrors.ProductAttributeValueErrors.NameRequired);
        if (name.Length > 256) throw new DomainException(DomainErrors.ProductAttributeValueErrors.NameTooLong);
    }
}