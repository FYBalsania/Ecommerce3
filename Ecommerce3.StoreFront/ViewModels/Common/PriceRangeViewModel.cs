using Ecommerce3.Contracts.DTO.StoreFront.Product;

namespace Ecommerce3.StoreFront.ViewModels.Common;

public record PriceRangeViewModel
{
    public required PriceRangeDTO PriceRange { get; init; }
    public decimal? SelectedMinPrice { get; init; }
    public decimal? SelectedMaxPrice { get; init; }
}