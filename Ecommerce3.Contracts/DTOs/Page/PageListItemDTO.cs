namespace Ecommerce3.Contracts.DTOs.Page;

public record PageListItemDTO(
    int Id, 
    string Discriminator,
    string? Path,
    int? BrandId,
    int? CategoryId,
    int? ProductId,
    int? ProductGroupId,
    bool IsActive,
    string CreatedUserFullName, 
    DateTime CreatedAt);