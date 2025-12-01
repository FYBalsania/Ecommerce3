namespace Ecommerce3.Contracts.DTOs;

public record ProductAttributeDateOnlyValueDTO : ProductAttributeValueDTO
{
    public DateOnly DateOnlyValue { get; set; }

    public ProductAttributeDateOnlyValueDTO(int id, string value, string slug, string display, string breadcrumb, int sortOrder, string createdUserFullName, DateTime createdAt, DateOnly dateOnlyValue) 
        : base(id, value, slug, display, breadcrumb, sortOrder, createdUserFullName, createdAt)
    {
        DateOnlyValue = dateOnlyValue;
    }
}