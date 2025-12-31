using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Product;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Admin.ViewModels.Product;

public record ProductsIndexViewModel
{
    public ProductFilter Filter { get; init; }
    public PagedResult<ProductListItemDTO> Products { get; init; }
}