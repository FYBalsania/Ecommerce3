namespace Ecommerce3.Domain.Enums;

[Flags]
public enum CustomerAddressInclude
{
    None = 0,
    Customer = 1 << 0,
}