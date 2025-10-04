namespace Ecommerce3.Application.DTOs.Brand;

public record BrandListItemDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public required string CreatedUserFullName { get; init; }
    public required DateTime CreatedAt { get; init; }
    
    public static BrandListItemDTO FromDomain(Domain.Entities.Brand brand)
    {
        return new BrandListItemDTO
        {
            Id = brand.Id,
            Name = brand.Name,
            Slug = brand.Slug,
            CreatedAt = brand.CreatedAt,
            CreatedUserFullName = brand.CreatedByUser!.FullName
        };
    }
}