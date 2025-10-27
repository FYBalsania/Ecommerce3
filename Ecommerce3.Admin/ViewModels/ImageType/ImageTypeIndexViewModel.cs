using cloudscribe.Pagination.Models;
using Ecommerce3.Contracts.DTOs.ImageType;
using Ecommerce3.Contracts.Filters;

namespace Ecommerce3.Admin.ViewModels.ImageType;

public record ImageTypeIndexViewModel
{
    public ImageTypeFilter Filter { get; init; }
    public PagedResult<ImageTypeListItemDTO> ImageTypes { get; init; }
    public string PageTitle { get; init; }
}