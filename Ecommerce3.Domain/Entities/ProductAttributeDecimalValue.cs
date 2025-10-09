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
}