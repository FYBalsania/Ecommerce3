namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class BrandErrors
    {
        public static readonly DomainError NameRequired = new("Brand.Name", "Name is required.");
    }
}