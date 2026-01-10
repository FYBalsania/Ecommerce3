using System.Net;

namespace Ecommerce3.Application.Commands.Admin.Country;

public record EditCountryCommand
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Iso2Code { get; init; }
    public required string Iso3Code { get; init; }
    public string? NumericCode { get; init; }
    public required bool IsActive { get; init; }
    public required int SortOrder { get; init; }
    public required int UpdatedBy { get; init; }
    public DateTime UpdatedAt { get; init; }
    public required IPAddress UpdatedByIp { get; init; }
}