using System.Net;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Commands.Admin.Product;

public record EditProductCommand
{
    public required int Id { get; init; }
    public required string SKU { get; init; }
    public required string? GTIN { get; init; }
    public required string? MPN { get; init; }
    public required string? MFC { get; init; }
    public required string? EAN { get; init; }
    public required string? UPC { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public required string Display { get; init; }
    public required string Breadcrumb { get; init; }
    public required string AnchorText { get; init; }
    public required string? AnchorTitle { get; init; }
    public required int BrandId { get; init; }
    public required int[] CategoryIds { get; init; }
    public required int? ProductGroupId { get; init; }
    public required IDictionary<int, int> Attributes { get; init;}
    public required string? ShortDescription { get; init; }
    public required string? FullDescription { get; init; }
    public required bool AllowReviews { get; init; }
    public required decimal Price { get; init; }
    public required decimal? OldPrice { get; init; }
    public required decimal? CostPrice { get; init; }
    public required decimal Stock { get; init; }
    public required decimal? MinStock { get; init; }
    public required bool ShowAvailability { get; init; }
    public required bool FreeShipping { get; init; }
    public required decimal AdditionalShippingCharge { get; init; }
    public required int UnitOfMeasureId { get; init; }
    public required decimal QuantityPerUnitOfMeasure { get; init; }
    public required int DeliveryWindowId { get; init; }
    public required decimal MinOrderQuantity { get; init; }
    public required decimal? MaxOrderQuantity { get; init; }
    public required bool IsFeatured { get; init; }
    public required bool IsNew { get; init; }
    public required bool IsBestSeller { get; init; }
    public required bool IsReturnable { get; init; }
    public required ProductStatus Status { get; init; }
    public required string? RedirectUrl { get; init; }
    public required int CountryOfOriginId { get; init; }
    public required decimal SortOrder { get; init; }
    public required string? H1 { get; init; }
    public required string MetaTitle { get; init; }
    public required string? MetaDescription { get; init; }
    public required string? MetaKeywords { get; init; }
    public int UpdatedBy { get; init; }
    public DateTime UpdatedAt { get; init; }
    public IPAddress UpdatedByIp { get; init; }
}