using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class CountryErrors
    {
        public static readonly DomainError InvalidId =
            new($"{nameof(Country)}.{nameof(Country.Id)}", "Country id is invalid.");
    }
}