namespace Ecommerce3.Contracts.DTOs;

public record ProductAttributeBooleanValueDTO : ProductAttributeValueDTO
{
    public bool BooleanValue { get; private set; }

    public ProductAttributeBooleanValueDTO(int id, string value, string slug, string display, string breadcrumb,
        decimal sortOrder, string createdUserFullName, DateTime createdAt, bool booleanValue)
        : base(id, value, slug, display, breadcrumb, sortOrder, createdUserFullName, createdAt)
    {
        BooleanValue = booleanValue;
    }
}