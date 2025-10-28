namespace Ecommerce3.Contracts.DTOs.ProductAttribute;

public class ProductAttributeDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; }
    public int ImageCount { get; set; }
    public string CreatedUserFullName { get; set; }
    public DateTime CreatedAt { get; set; }
}