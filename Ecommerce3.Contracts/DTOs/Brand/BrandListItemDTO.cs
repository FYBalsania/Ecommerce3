namespace Ecommerce3.Contracts.DTOs.Brand;

public record BrandListItemDTO(int Id, string Name, string Slug, string CreatedUserFullName, DateTime CreatedAt);