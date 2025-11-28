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

    public ProductAttribute(string name, string slug, string display, string breadcrumb, DataType dataType,
        int sortOrder, int createdBy, string createdByIp)
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
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;

        if (DataType == DataType.Boolean)
        {
            _values.Add(new ProductAttributeBooleanValue("Yes", "yes", "Yes", "Yes", true, 1, createdBy, DateTime.Now,
                createdByIp));
            _values.Add(new ProductAttributeBooleanValue("No", "no", "No", "No", false, 2, createdBy, DateTime.Now,
                createdByIp));
        }
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
        _values.Add(value);
    }
}