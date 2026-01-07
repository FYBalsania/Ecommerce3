namespace Ecommerce3.Contracts.DTO.StoreFront.Product;

public record PriceRangeDTO
{
    public required decimal MinPrice { get; init; }
    public required decimal MaxPrice { get; init; }
}