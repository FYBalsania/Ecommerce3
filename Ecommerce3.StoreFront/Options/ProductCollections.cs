namespace Ecommerce3.StoreFront.Options;

public record ProductCollections
{
    public required string Name { get; init; }
    public required string[] ProductSKUs { get; init; }
}