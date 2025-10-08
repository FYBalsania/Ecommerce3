namespace Ecommerce3.Contracts.DTOs.Brand;

public record BrandListItemDTO(
    int Id, 
    string Name, 
    string Slug, 
    int SortOrder,
    bool IsActive,
    int ImageCount,
    string CreatedUserFullName, 
    DateTime CreatedAt);