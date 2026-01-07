namespace Ecommerce3.Contracts.DTO.StoreFront.Product;

public record ProductAttributeFacetDTO
{
    public required int AttributeId { get; init; }
    public required string AttributeDisplay { get; init; }
    public required int AttributeValueId { get; init; }
    public required string AttributeValueDisplay { get; init; }
}