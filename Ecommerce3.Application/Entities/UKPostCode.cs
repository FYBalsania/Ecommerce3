using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Application.Entities;

public class UKPostCode : PostalCode
{
    public string Zone1 { get; init; }
    public string Zone2 { get; init; }
    public string Ward { get; init; }

    public UKPostCode()
    {
        Zone1 = Zone2 = Ward = string.Empty;
    }
}