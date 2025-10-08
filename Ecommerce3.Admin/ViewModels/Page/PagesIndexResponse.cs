using Ecommerce3.Contracts.DTOs.Page;

namespace Ecommerce3.Admin.ViewModels.Page;

public record PagesIndexResponse
{
    public string? PageName { get; init; }
    public int PageNumber { get; init; }
    public int PageSize { get; init; }
    public IEnumerable<PageListItemDTO> PageListItems { get; init; }
    public int PageListItemsCount { get; init; }
}