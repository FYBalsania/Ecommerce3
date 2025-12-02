using System.Globalization;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductAttributeDecimalValue : ProductAttributeValue
{
    public decimal DecimalValue { get; private set; }

    private ProductAttributeDecimalValue() : base()
    {
    }

    public ProductAttributeDecimalValue(decimal decimalValue, string slug, string display, string breadcrumb,
        int sortOrder, int createdBy, DateTime createdAt, string createdByIp)
        : base(decimalValue.ToString(CultureInfo.InvariantCulture), slug, display, breadcrumb, sortOrder, createdBy,
            createdAt, createdByIp)
    {
        DecimalValue = decimalValue;
    }

    internal bool Update(decimal decimalValue, string slug, string display, string breadcrumb, int sortOrder,
        int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        if (!base.Update(decimalValue.ToString(CultureInfo.InvariantCulture), slug, display, breadcrumb, sortOrder,
                updatedBy, updatedAt, updatedByIp) && DecimalValue == decimalValue) return false;

        DecimalValue = decimalValue;

        return true;
    }
}