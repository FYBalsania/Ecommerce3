namespace Ecommerce3.Contracts.DTO.StoreFront.UOM;

public record UOMFacetDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal QtyPerUOM { get; init; }
}