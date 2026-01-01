using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.Page;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Admin.ViewModels.Page;

public record PagesIndexViewModel
{
    public PageFilter Filter { get; init; }
    public PagedResult<PageListItemDTO> Pages { get; init; } 
}