namespace Ecommerce3.Domain.Entities;

public sealed record CustomerAddressHistory
{
    public string? Type { get; init; }
    public string? FullName { get; init; }
    public string? PhoneNumber  { get; init; }
    public string? CompanyName  { get; init; }
    public required string AddressLine1 { get; init; }
    public string? AddressLine2 { get; init; }
    public required string City { get; init; }
    public required string StateOrProvince  { get; init; }
    public required string PostalCode { get; init; }
    public string? Landmark { get; init; }
    public DateTime UpdatedAt { get; init; }
    public required string UpdatedByIp { get; init; }
}