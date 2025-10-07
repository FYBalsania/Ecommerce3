namespace Ecommerce3.Contracts.DTOs.Brand;

public record BrandListItemDTO(
    int Id, 
    string Name, 
    string Slug, 
    int SortOrder,
    string CreatedUserFullName, 
    DateTime CreatedAt);