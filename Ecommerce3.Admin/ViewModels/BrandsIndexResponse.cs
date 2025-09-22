using Ecommerce3.Application.DTOs;
using Ecommerce3.Domain.Models;

namespace Ecommerce3.Admin.ViewModels;

public record BrandsIndexResponse
{
    public required string? Name { get; init; }
    public required int PageNumber { get; init; }
    public required int PageSize { get; init; }
    public required IEnumerable<BrandListItemDTO> BrandListItems { get; init; }
    public required int BrandListItemsCount { get; init; }
}