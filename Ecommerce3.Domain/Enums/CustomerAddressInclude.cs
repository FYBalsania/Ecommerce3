namespace Ecommerce3.Domain.Enums;

[Flags]
public enum CustomerAddressInclude
{
    Customer = 1 << 0,
}