using System.Net;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Commands.ProductAttribute;

public record AddProductAttributeCommand
{
    public string Name { get; init; }
    public string Slug { get; init; }
    public string Display { get; init; }
    public string Breadcrumb { get; init; }
    public DataType DataType { get; init; }
    public int SortOrder { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public IPAddress CreatedByIp { get; init; }
}