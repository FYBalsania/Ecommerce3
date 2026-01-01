namespace Ecommerce3.Contracts.DTO.StoreFront.ProductListPage;

public record PLPParentCategoryDTO
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public required string Display { get; init; }
    public required string Breadcrumb { get; init; }
    public required string AnchorText { get; init; }
    public required string? AnchorTitle { get; init; }
    public required string? GoogleCategory { get; init; }
    public required string? ShortDescription { get; init; }
    public required string? FullDescription { get; init; }
    public required IReadOnlyList<PLPChildCategoryDTO> Children { get; init; }
}