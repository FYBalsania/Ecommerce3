namespace Ecommerce3.Domain.Entities;

public sealed record CustomerHistory()
{
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public string? CompanyName { get; init; }
    public string? PhoneNumber { get; init; }
    public DateTime UpdatedAt { get; init; }
    public required string UpdatedByIp { get; init; }
}