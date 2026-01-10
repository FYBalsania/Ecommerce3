using System.ComponentModel.DataAnnotations;
using System.Net;
using Ecommerce3.Application.Commands.Admin.Country;
using Ecommerce3.Contracts.DTO.Admin.Country;

namespace Ecommerce3.Admin.ViewModels.Country;

public class EditCountryViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = $"{nameof(Name)} is required.")]
    [StringLength(256, MinimumLength = 1, ErrorMessage = $"{nameof(Name)} must be between 1 and 256 characters.")]
    [Display(Name = nameof(Name))]
    public string Name { get; set; }
    
    [Required(ErrorMessage = $"{nameof(Name)} is required.")]
    [StringLength(2, MinimumLength = 1, ErrorMessage = "ISO2 code must be between 1 and 2 characters.")]
    [Display(Name = "ISO2 code")]
    public string Iso2Code { get; set; }
    
    [Required(ErrorMessage = $"{nameof(Name)} is required.")]
    [StringLength(3, MinimumLength = 1, ErrorMessage = "ISO3 code must be between 1 and 3 characters.")]
    [Display(Name = "ISO3 code")]
    public string Iso3Code { get; set; }
    
    [StringLength(3, MinimumLength = 1, ErrorMessage = "Numeric code must be between 1 and 3 characters.")]
    [Display(Name = "Numeric code")]
    public string? NumericCode { get; set; }
    
    [Required(ErrorMessage = "Is active is required.")]
    [Display(Name = "Is active")]
    public bool IsActive { get; set; }
    
    [Required(ErrorMessage = "Sort order is required.")]
    [Display(Name = "Sort order")]
    public int SortOrder { get; set; }
    
    public EditCountryCommand ToCommand(int updatedBy, DateTime updatedAt, IPAddress updatedByIp)
    {
        return new EditCountryCommand()
        {
            Id = Id,
            Name = Name,
            Iso2Code =  Iso2Code,
            Iso3Code =  Iso3Code,
            NumericCode = NumericCode,
            IsActive = IsActive,
            SortOrder = SortOrder,
            UpdatedBy = updatedBy,
            UpdatedAt = updatedAt,
            UpdatedByIp = updatedByIp,
        };
    }

    public static EditCountryViewModel FromDTO(CountryDTO dto)
    {
        return new EditCountryViewModel()
        {
            Id = dto.Id,
            Name = dto.Name,
            Iso2Code =  dto.Iso2Code,
            Iso3Code =  dto.Iso3Code,
            NumericCode =  dto.NumericCode,
            IsActive = dto.IsActive,
            SortOrder = dto.SortOrder,
        };
    }
}