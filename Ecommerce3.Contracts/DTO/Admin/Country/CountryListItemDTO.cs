namespace Ecommerce3.Contracts.DTO.Admin.Country;

public record CountryListItemDTO
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Iso2Code { get; init; }
    public string Iso3Code { get; init; }
    public string? NumericCode { get; init; }
    public bool IsActive { get; init; }
    public int SortOrder { get; init; }
    public string CreatedUserFullName { get; init; }
    public DateTime CreatedAt { get; init; }
}