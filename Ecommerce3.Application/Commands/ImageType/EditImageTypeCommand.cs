using System.Net;

namespace Ecommerce3.Application.Commands.ImageType;

public record EditImageTypeCommand
{
    public int Id { get; init; }
    public string? Entity { get; init; }
    public string Name { get; init; }
    public string Slug { get; init; }
    public string? Description { get; init; }
    public bool IsActive { get; init; }
    public int UpdatedBy { get; init; }
    public DateTime UpdatedAt { get; init; }
    public IPAddress UpdatedByIp { get; init; }
}