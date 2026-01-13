using System.Net;

namespace Ecommerce3.Application.Commands.ProductAttribute;

public class AddProductAttributeDecimalValueCommand
{
    public required int ProductAttributeId { get; init; }
    public decimal DecimalValue { get; init; }
    public required string Slug { get; init; }
    public required string Display { get; init; }
    public required string Breadcrumb { get; init; }
    public required int SortOrder { get; init; }
    public required int CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required IPAddress CreatedByIp { get; init; }
}