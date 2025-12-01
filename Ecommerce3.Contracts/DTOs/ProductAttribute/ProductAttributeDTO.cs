using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.DTOs.ProductAttribute;

public class ProductAttributeDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Display { get; set; }
    public string Breadcrumb { get; set; }
    public int SortOrder { get; set; }
    public DataType DataType { get; set; }
    public IReadOnlyList<ProductAttributeValueDTO> Values { get; set; } = [];
}