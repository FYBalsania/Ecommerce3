namespace Ecommerce3.Application.Entities;

public interface IPostalCodeValidator
{
    string ValidateAndNormalize(string postalCode, string countryCode);
}