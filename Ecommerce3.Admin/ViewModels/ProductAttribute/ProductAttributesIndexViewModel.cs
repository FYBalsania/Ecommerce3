using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.ProductAttribute;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Admin.ViewModels.ProductAttribute;

public record ProductAttributesIndexViewModel
{
    public ProductAttributeFilter Filter { get; init; }
    public PagedResult<ProductAttributeListItemDTO> ProductAttributes { get; init; }
    public string PageTitle { get; init; }
}