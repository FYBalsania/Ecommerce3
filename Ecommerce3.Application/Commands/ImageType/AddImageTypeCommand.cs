using System.Net;

namespace Ecommerce3.Application.Commands.ImageType;

public record AddImageTypeCommand
{
    public string? Entity { get; init; }
    public string Name { get; init; }
    public string Slug { get; init; }
    public string? Description { get; init; }
    public bool IsActive { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public IPAddress CreatedByIp { get; init; }
}