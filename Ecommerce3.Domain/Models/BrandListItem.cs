namespace Ecommerce3.Domain.Models;

public record BrandListItem(
    int Id,
    string Name,
    string Slug,
    bool IsActive,
    int SortOrder,
    string CreatedBy,
    DateTime CreatedAt
);