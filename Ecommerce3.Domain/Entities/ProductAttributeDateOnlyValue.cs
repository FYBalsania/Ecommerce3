namespace Ecommerce3.Domain.Entities;

public sealed class ProductAttributeDateOnlyValue : ProductAttributeValue
{
    public DateOnly DateOnlyValue { get; private set; }

    public ProductAttributeDateOnlyValue(string value, string slug, string display, string breadcrumb,
        DateOnly dateOnlyValue, int sortOrder, int createdBy, DateTime createdAt, string createdByIp)
        : base(value, slug, display, breadcrumb, sortOrder, createdBy, createdAt, createdByIp)
    {
        DateOnlyValue = dateOnlyValue;
    }
}