using System.Net;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Commands.Image;

public record EditImageCommand
{
    public required string ParentEntityType { get; init; }
    public required string ParentEntityId { get; init; }
    public required string ImageEntityType { get; init; }
    public int Id { get; init; }
    public required int ImageTypeId { get; init; }
    public required ImageSize ImageSize { get; init; }
    public string? AltText { get; init; }
    public string? Title { get; init; }
    public required ImageLoading Loading { get; init; }
    public string? Link { get; init; }
    public string? LinkTarget { get; init; }
    public required int SortOrder { get; init; }
    public required string ImageFolderPath { get; init; }
    public required int UpdatedBy { get; init; }
    public required DateTime UpdatedAt { get; init; }
    public required IPAddress UpdatedByIp { get; init; }
}