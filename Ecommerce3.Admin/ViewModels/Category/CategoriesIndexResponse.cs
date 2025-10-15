using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Category;
using Ecommerce3.Contracts.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce3.Admin.ViewModels.Category;

public record CategoriesIndexResponse
{
    public CategoryFilter Filter { get; init; }
    public required SelectList Parents { get; init; }
    public PagedResult<CategoryListItemDTO> Categories { get; init; }
    public string PageTitle { get; init; }
}