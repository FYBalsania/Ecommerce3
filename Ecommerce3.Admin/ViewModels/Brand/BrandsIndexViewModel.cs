using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Brand;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Admin.ViewModels.Brand;

public record BrandsIndexViewModel
{
    public BrandFilter Filter { get; init; }
    public PagedResult<BrandListItemDTO> Brands { get; init; }
    public string PageTitle { get; init; }
}