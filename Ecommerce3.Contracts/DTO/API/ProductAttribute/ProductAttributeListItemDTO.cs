using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.DTO.API.ProductAttribute;

public record ProductAttributeListItemDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Display { get; init; }
    public required string Breadcrumb { get; init; }
    public required DataType DataType { get; init; }
    public required decimal SortOrder { get; init; }
}