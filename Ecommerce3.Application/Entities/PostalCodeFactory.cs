using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Application.Entities;

public static class PostalCodeFactory
{
    public static PostalCode Create(string countryCode)
    {
        return countryCode.ToUpperInvariant()
            switch
            {
                "IN" => new IndiaPinCode(),
                _ => new UKPostCode()
            };
    }
}