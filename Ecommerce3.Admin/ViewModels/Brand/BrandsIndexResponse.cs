using Ecommerce3.Contracts.DTOs.Brand;

namespace Ecommerce3.Admin.ViewModels.Brand;

public record BrandsIndexResponse
{
    public string? BrandName { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public IEnumerable<BrandListItemDTO> BrandListItems { get; init; }
    public int BrandListItemsCount { get; init; }
}