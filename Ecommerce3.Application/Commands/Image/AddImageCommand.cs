using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Commands.Image;

public record AddImageCommand
{
    public required Type ParentEntityType { get; init; }
    public required int ParentEntityId { get; init; }
    public required Type ImageEntityType { get; init; }
    public required int ImageTypeId { get; init; }
    public required ImageSize ImageSize { get; init; }
    public required byte[] File { get; init; }
    public required int MaxFileSizeKb {get; init;}
    public required string FileName { get; init; }
    public string? AltText { get; init; }
    public string? Title { get; init; }
    public required string Loading { get; init; }
    public string? Link { get; init; }
    public string? LinkTarget { get; init; }
    public required int SortOrder { get; init; }
    public required string TempImageFolderPath { get; init; }
    public required string ImageFolderPath { get; init; }
    public required int CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string CreatedByIp { get; init; }
}