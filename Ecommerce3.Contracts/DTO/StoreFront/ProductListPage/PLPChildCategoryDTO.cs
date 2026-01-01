using Ecommerce3.Contracts.DTO.StoreFront.Image;

namespace Ecommerce3.Contracts.DTO.StoreFront.ProductListPage;

public record PLPChildCategoryDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public required string Display { get; init; }
    public required string Breadcrumb { get; init; }
    public required string AnchorText { get; init; }
    public required string? AnchorTitle { get; init; }
    public required ImageDTO? ListItemImage { get; init; }
}