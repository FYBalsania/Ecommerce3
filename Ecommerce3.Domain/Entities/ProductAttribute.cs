using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductAttribute : Entity, ICreatable, IUpdatable, IDeletable
{
    private readonly List<ProductAttributeValue> _values = [];

    public string Name { get; private set; }
    public string Slug { get; private set; }
    public string Display { get; private set; }
    public string Breadcrumb { get; private set; }
    public DataType DataType { get; private set; }
    public int SortOrder { get; private set; }
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

    public IReadOnlyList<ProductAttributeValue> Values => _values;
    
    private ProductAttribute(){}

    public ProductAttribute(string name, string slug, string display, string breadcrumb, DataType dataType,
        int sortOrder, int createdBy, DateTime createdAt, string createdByIp)
    {
        ValidateName(name);
        ValidateSlug(slug);
        ValidateDisplay(display);
        ValidateBreadcrumb(breadcrumb);
        ValidateCreatedBy(createdBy);
        ValidateCreatedByIp(createdByIp);

        Name = name;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        DataType = dataType;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;

        if (DataType != DataType.Boolean) return;

        _values.Add(new ProductAttributeBooleanValue(true, "yes", "Yes", "Yes", 1, createdBy, createdAt,
            createdByIp));
        _values.Add(new ProductAttributeBooleanValue(false, "no", "No", "No", 2, createdBy, createdAt,
            createdByIp));
    }

    public bool Update(string name, string slug, string display, string breadcrumb, int sortOrder, int updatedBy,
        DateTime updatedAt, string updatedByIp)
    {
        ValidateName(name);
        ValidateSlug(slug);
        ValidateDisplay(display);
        ValidateBreadcrumb(breadcrumb);
        ValidateUpdatedBy(updatedBy);
        ValidateUpdatedByIp(updatedByIp);

        if (Name == name && Slug == slug && Display == display && Breadcrumb == breadcrumb &&
            SortOrder == sortOrder) return false;

        Name = name;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;

        return true;
    }

    public void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }

    private static void ValidateCreatedByIp(string createdByIp)
    {
        if (string.IsNullOrWhiteSpace(createdByIp))
            throw new DomainException(DomainErrors.ProductAttributeErrors.CreatedByIpRequired);
        if (createdByIp.Length > 128) throw new DomainException(DomainErrors.ProductAttributeErrors.CreatedByIpTooLong);
    }

    private static void ValidateCreatedBy(int createdBy)
    {
        if (createdBy <= 0) throw new DomainException(DomainErrors.ProductAttributeErrors.InvalidCreatedBy);
    }

    private static void ValidateUpdatedBy(int updatedBy)
    {
        if (updatedBy <= 0) throw new DomainException(DomainErrors.ProductAttributeErrors.InvalidUpdatedBy);
    }

    private static void ValidateBreadcrumb(string breadcrumb)
    {
        if (string.IsNullOrWhiteSpace(breadcrumb))
            throw new DomainException(DomainErrors.ProductAttributeErrors.BreadcrumbRequired);
        if (breadcrumb.Length > 256) throw new DomainException(DomainErrors.ProductAttributeErrors.BreadcrumbTooLong);
    }

    private static void ValidateDisplay(string display)
    {
        if (string.IsNullOrWhiteSpace(display))
            throw new DomainException(DomainErrors.ProductAttributeErrors.DisplayRequired);
        if (display.Length > 256) throw new DomainException(DomainErrors.ProductAttributeErrors.DisplayTooLong);
    }

    private static void ValidateSlug(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
            throw new DomainException(DomainErrors.ProductAttributeErrors.SlugRequired);
        if (slug.Length > 256) throw new DomainException(DomainErrors.ProductAttributeErrors.SlugTooLong);
    }

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException(DomainErrors.ProductAttributeErrors.NameRequired);
        if (name.Length > 256) throw new DomainException(DomainErrors.ProductAttributeErrors.NameTooLong);
    }

    private static void ValidateUpdatedByIp(string updatedByIp)
    {
        if (string.IsNullOrWhiteSpace(updatedByIp))
            throw new DomainException(DomainErrors.ProductAttributeErrors.UpdatedByIpRequired);
        if (updatedByIp.Length > 128) throw new DomainException(DomainErrors.ProductAttributeErrors.UpdatedByIpTooLong);
    }

    public void AddValue(ProductAttributeValue value)
    {
        if (value is null) throw new DomainException(DomainErrors.Common.Null);
        if (value is ProductAttributeBooleanValue)
            throw new DomainException(DomainErrors.Common.OutOfRange);

        var stringComparer = StringComparer.OrdinalIgnoreCase;
        var exists = false;

        switch (value)
        {
            case ProductAttributeDecimalValue:
            {
                exists = _values.Any(x =>
                    ((ProductAttributeDecimalValue)x).DecimalValue == ((ProductAttributeDecimalValue)value).DecimalValue);
                break;
            }
            case ProductAttributeDateOnlyValue:
            {
                exists = _values.Any(x =>
                    ((ProductAttributeDateOnlyValue)x).DateOnlyValue ==
                    ((ProductAttributeDateOnlyValue)value).DateOnlyValue);
                break;
            }
            case ProductAttributeValue or ProductAttributeColourValue:
            {
                exists = _values.Any(x => stringComparer.Equals(x.Value, value.Value));
                break;
            }
            default:
                throw new DomainException(DomainErrors.Common.OutOfRange);
        }
        if (exists) throw new DomainException(DomainErrors.ProductAttributeValueErrors.DuplicateValue);

        exists = _values.Any(x => stringComparer.Equals(x.Slug, value.Slug));
        if (exists) throw new DomainException(DomainErrors.ProductAttributeValueErrors.DuplicateSlug);

        _values.Add(value);
    }

    public bool UpdateValue(int id, string value, string slug, string display, string breadcrunb, int sortOrder,
        int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        var stringComparer = StringComparer.OrdinalIgnoreCase;

        var valueToUpdate = _values.FirstOrDefault(x => x.Id == id);
        if (valueToUpdate is null) throw new DomainException(DomainErrors.ProductAttributeValueErrors.InvalidId);

        var exists = _values.Any(x => x.Id != id && stringComparer.Equals(x.Value, value));
        if (exists) throw new DomainException(DomainErrors.ProductAttributeValueErrors.DuplicateValue);

        exists = _values.Any(x => x.Id != id && stringComparer.Equals(x.Slug, slug));
        return exists
            ? throw new DomainException(DomainErrors.ProductAttributeValueErrors.DuplicateSlug)
            : valueToUpdate.Update(value, slug, display, breadcrunb, sortOrder, updatedBy, updatedAt, updatedByIp);
    }

    //Update decimal value.
    public bool UpdateValue(int id, decimal decimalValue, string slug, string display, string breadcrunb, int sortOrder,
        int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        var stringComparer = StringComparer.OrdinalIgnoreCase;

        var valueToUpdate = _values.FirstOrDefault(x => x.Id == id);
        if (valueToUpdate is null) throw new DomainException(DomainErrors.ProductAttributeValueErrors.InvalidId);

        var exists = _values
            .Any(x => x.Id != id && ((ProductAttributeDecimalValue)x).DecimalValue == decimalValue);
        if (exists) throw new DomainException(DomainErrors.ProductAttributeValueErrors.DuplicateValue);

        exists = _values.Any(x => x.Id != id && stringComparer.Equals(x.Slug, slug));
        if (exists) throw new DomainException(DomainErrors.ProductAttributeValueErrors.DuplicateSlug);

        return ((ProductAttributeDecimalValue)valueToUpdate).Update(decimalValue, slug, display, breadcrunb, sortOrder,
            updatedBy, updatedAt, updatedByIp);
    }

    //Update date-only value.
    public bool UpdateValue(int id, DateOnly dateOnlyValue, string slug, string display, string breadcrunb,
        int sortOrder, int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        var stringComparer = StringComparer.OrdinalIgnoreCase;

        var valueToUpdate = _values.FirstOrDefault(x => x.Id == id);
        if (valueToUpdate is null) throw new DomainException(DomainErrors.ProductAttributeValueErrors.InvalidId);

        var exists = _values
            .Any(x => x.Id != id && ((ProductAttributeDateOnlyValue)x).DateOnlyValue == dateOnlyValue);
        if (exists) throw new DomainException(DomainErrors.ProductAttributeValueErrors.DuplicateValue);

        exists = _values.Any(x => x.Id != id && stringComparer.Equals(x.Slug, slug));
        if (exists) throw new DomainException(DomainErrors.ProductAttributeValueErrors.DuplicateSlug);

        return ((ProductAttributeDateOnlyValue)valueToUpdate).Update(dateOnlyValue, slug, display, breadcrunb,
            sortOrder, updatedBy, updatedAt, updatedByIp);
    }

    //Update boolean value.
    public bool UpdateValue(int id, string slug, string display, string breadcrunb, int sortOrder,
        int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        var stringComparer = StringComparer.OrdinalIgnoreCase;

        var valueToUpdate = _values.FirstOrDefault(x => x.Id == id);
        if (valueToUpdate is null) throw new DomainException(DomainErrors.ProductAttributeValueErrors.InvalidId);

        var exists = _values.Any(x => x.Id != id && stringComparer.Equals(x.Slug, slug));
        if (exists) throw new DomainException(DomainErrors.ProductAttributeValueErrors.DuplicateSlug);

        return ((ProductAttributeBooleanValue)valueToUpdate).Update(slug, display, breadcrunb, sortOrder, updatedBy,
            updatedAt, updatedByIp);
    }

    //Update colour value.
    public bool UpdateValue(int id, string value, string slug, string display, string breadcrunb, string? hexCode,
        string colourFamily, string? colourFamilyHexCode, int sortOrder, int updatedBy, DateTime updatedAt,
        string updatedByIp)
    {
        var stringComparer = StringComparer.OrdinalIgnoreCase;

        var valueToUpdate = _values.FirstOrDefault(x => x.Id == id);
        if (valueToUpdate is null) throw new DomainException(DomainErrors.ProductAttributeValueErrors.InvalidId);

        var exists = _values.Any(x => x.Id != id && stringComparer.Equals(x.Value, value));
        if (exists) throw new DomainException(DomainErrors.ProductAttributeValueErrors.DuplicateValue);

        exists = _values.Any(x => x.Id != id && stringComparer.Equals(x.Slug, slug));
        if (exists) throw new DomainException(DomainErrors.ProductAttributeValueErrors.DuplicateSlug);

        return ((ProductAttributeColourValue)valueToUpdate).Update(value, slug, display, breadcrunb, sortOrder,
            hexCode, colourFamily, colourFamilyHexCode, updatedBy, updatedAt, updatedByIp);
    }

    public void DeleteValue(int id, int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        var valueToDelete = _values.FirstOrDefault(x => x.Id == id);
        if (valueToDelete is null) throw new DomainException(DomainErrors.ProductAttributeValueErrors.InvalidId);

        valueToDelete.Delete(deletedBy, deletedAt, deletedByIp);
        _values.Remove(valueToDelete);
    }
}