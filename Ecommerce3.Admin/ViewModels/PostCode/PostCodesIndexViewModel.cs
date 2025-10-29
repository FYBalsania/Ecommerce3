using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.PostCode;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Admin.ViewModels.PostCode;

public record PostCodesIndexViewModel
{
    public PostCodeFilter Filter { get; init; }
    public PagedResult<PostCodeListItemDTO> PostCodes { get; init; }
    public string PageTitle { get; init; }
}