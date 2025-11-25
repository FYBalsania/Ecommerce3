namespace Ecommerce3.Contracts.DTOs;

public class ProductAttributeDecimalValueDTO : ProductAttributeValueDTO
{
    public decimal DecimalValue { get; private set; }

    public ProductAttributeDecimalValueDTO(int id, string value, string slug, string display, string breadcrumb, int sortOrder, string createdUserFullName, DateTime createdAt, decimal decimalValue) 
        : base(id, value, slug, display, breadcrumb, sortOrder, createdUserFullName, createdAt)
    {
        DecimalValue = decimalValue;
    }
}