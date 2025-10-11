using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Category;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Admin.ViewModels.Category;

public record CategoriesIndexResponse
{
    public CategoryFilter Filter { get; init; }
    public PagedResult<CategoryListItemDTO> Categories { get; init; }
    public string PageTitle { get; init; }
}