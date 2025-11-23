using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Application.Entities;

public sealed class IndiaPinCode : PostalCode
{
    public string State { get; init; }
    public string District { get; init; }
    public string City { get; init; }

    public IndiaPinCode()
    {
        State = District = City = string.Empty;
    }
}