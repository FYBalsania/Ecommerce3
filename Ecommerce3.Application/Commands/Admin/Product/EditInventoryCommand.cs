using System.Net;

namespace Ecommerce3.Application.Commands.Admin.Product;

public record EditInventoryCommand
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required decimal Price { get; init; }
    public decimal? OldPrice { get; init; }
    public required decimal Stock { get; init; }
    public int UpdatedBy { get; init; }
    public DateTime UpdatedAt { get; init; }
    public IPAddress UpdatedByIp { get; init; }
    public string? ReturnUrl { get; init; }
}