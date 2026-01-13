using System.Net;

namespace Ecommerce3.Application.Commands.PostCode;

public record AddPostCodeCommand
{
    public int Id { get; init; }
    public string Code { get; init; }
    public bool IsActive { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public IPAddress CreatedByIp { get; init; }
}