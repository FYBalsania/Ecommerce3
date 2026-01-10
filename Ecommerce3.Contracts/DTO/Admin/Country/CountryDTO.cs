namespace Ecommerce3.Contracts.DTO.Admin.Country;

public class CountryDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Iso2Code { get; set; }
    public string Iso3Code { get; set; }
    public string? NumericCode { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
}