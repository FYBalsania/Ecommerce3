namespace Ecommerce3.Domain.Models;

public record ProductReference(
    int Id,
    string SKUCode,
    string? GTIN,
    string? MPN,
    string? MFC,
    string? EAN,
    string? UPC,
    string Name,
    string Slug,
    string Display,
    decimal Price,
    decimal? OldPrice,
    decimal? CostPrice,
    bool FreeShipping,
    decimal AdditionalShippingCharge,
    decimal WeightKgs,
    bool Returnable,
    string? ReturnPolicy);