using System.Net;

namespace Ecommerce3.Application.Commands.PostCode;

public record EditPostCodeCommand
{
    public int Id { get; init; }
    public string Code { get; init; }
    public bool IsActive { get; init; }
    public int UpdatedBy { get; init; }
    public DateTime UpdatedAt { get; init; }
    public IPAddress UpdatedByIp { get; init; }
}