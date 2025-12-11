using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.DTO.StoreFront.Image;

public record ImageDTO
{
    public required int Id { get; init; }
    public required string FileName { get; init ; }
    public required string FileExtension { get; init; }
    public required int ImageTypeId { get; init; }
    public required string ImageTypeName { get; init; }
    public required string ImageTypeSlug { get; init; }
    public required ImageSize Size { get; init; }
    public required string? AltText { get; init; }
    public required string? Title { get; init; }
    public required ImageLoading Loading { get; init; }
    public required string? Link { get; init; }
    public required string? LinkTarget { get; init; }
    public required int SortOrder { get; init; }
}