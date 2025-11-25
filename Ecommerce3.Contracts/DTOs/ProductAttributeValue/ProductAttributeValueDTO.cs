using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Contracts.DTOs;

public abstract class ProductAttributeValueDTO
{
    public int Id { get; private set; }
    public string Value { get; private set; }
    public string Slug { get;  private set; }
    public string Display { get; private set; }
    public string Breadcrumb { get; private set; }
    public int SortOrder { get; private set; }
    public string CreatedUserFullName { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ProductAttributeValueDTO(int id, string value, string slug, string display, string breadcrumb, 
        int sortOrder, string createdUserFullName, DateTime createdAt)
    {
        Id = id;
        Value = value;
        Slug = slug;
        Display = display;
        Breadcrumb = breadcrumb;
        SortOrder = sortOrder;
        CreatedUserFullName = createdUserFullName;
        CreatedAt = createdAt;
    }
}