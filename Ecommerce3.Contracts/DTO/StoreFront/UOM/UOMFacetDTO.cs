namespace Ecommerce3.Contracts.DTO.StoreFront.UOM;

public record UOMFacetDTO
{
    public required int Id { get; init; }
    public required string SingularName { get; init; }
    public required string PluralName { get; init; }
    public required decimal QtyPerUOM { get; init; }
    public required byte DecimalPlaces { get; init; }
}