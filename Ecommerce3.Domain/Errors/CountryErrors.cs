using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class CountryErrors
    {
        public static readonly DomainError InvalidId =
            new($"{nameof(Country)}.{nameof(Country.Id)}", "Country id is invalid.");
        
        public static readonly DomainError NameRequired =
            new($"{nameof(Country)}.{nameof(Country.Name)}", "Name is required.");

        public static readonly DomainError NameTooLong =
            new($"{nameof(Country)}.{nameof(Country.Name)}", "Name cannot exceed 256 characters.");

        public static readonly DomainError DuplicateName =
            new($"{nameof(Country)}.{nameof(Country.Name)}", "Duplicate name.");
        
        public static readonly DomainError Iso2CodeRequired =
            new($"{nameof(Country)}.{nameof(Country.Iso2Code)}", "ISO2 code is required.");

        public static readonly DomainError Iso2CodeTooLong =
            new($"{nameof(Country)}.{nameof(Country.Iso2Code)}", "ISO2 code cannot exceed 2 characters.");

        public static readonly DomainError DuplicateIso2Code =
            new($"{nameof(Country)}.{nameof(Country.Iso2Code)}", "Duplicate ISO2 code.");
        
        public static readonly DomainError Iso3CodeRequired =
            new($"{nameof(Country)}.{nameof(Country.Iso3Code)}", "ISO3 code is required.");

        public static readonly DomainError Iso3CodeTooLong =
            new($"{nameof(Country)}.{nameof(Country.Iso3Code)}", "ISO3 code cannot exceed 3 characters.");

        public static readonly DomainError DuplicateIso3Code =
            new($"{nameof(Country)}.{nameof(Country.Iso3Code)}", "Duplicate ISO3 code.");

        public static readonly DomainError NumericCodeTooLong =
            new($"{nameof(Country)}.{nameof(Country.NumericCode)}", "Numeric code cannot exceed 3 characters.");

        public static readonly DomainError DuplicateNumericCode =
            new($"{nameof(Country)}.{nameof(Country.NumericCode)}", "Duplicate Numeric code.");
        
        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(Country)}.{nameof(Country.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(Country)}.{nameof(Country.UpdatedBy)}", "Updated by is invalid.");
    }
}