using Ecommerce3.Contracts.DTOs.Brand;

namespace Ecommerce3.Admin.ViewModels.Brand;

public record BrandsIndexResponse
{
    public required string? Name { get; init; }
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
    public required IEnumerable<BrandListItemDTO> BrandListItems { get; init; }
    public required int BrandListItemsCount { get; init; }
}