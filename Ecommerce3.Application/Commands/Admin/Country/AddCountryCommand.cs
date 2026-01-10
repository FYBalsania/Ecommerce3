using System.Net;

namespace Ecommerce3.Application.Commands.Admin.Country;

public record AddCountryCommand
{
    public required string Name { get; init; }
    public required string Iso2Code { get; init; }
    public required string Iso3Code { get; init; }
    public string? NumericCode { get; init; }
    public required bool IsActive { get; init; }
    public required int SortOrder { get; init; }
    public required int CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required IPAddress CreatedByIp { get; init; }
}