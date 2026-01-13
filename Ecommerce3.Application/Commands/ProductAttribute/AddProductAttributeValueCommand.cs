using System.Net;

namespace Ecommerce3.Application.Commands.ProductAttribute;

public record AddProductAttributeValueCommand
{
    public required int ProductAttributeId { get; init; }
    public required string Value { get; init; }
    public required string Slug { get; init; }
    public required string Display { get; init; }
    public required string Breadcrumb { get; init; }
    public required int SortOrder { get; init; }
    public required int CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required IPAddress CreatedByIp { get; init; }
}