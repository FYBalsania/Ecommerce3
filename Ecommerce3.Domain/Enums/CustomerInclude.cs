namespace Ecommerce3.Domain.Enums;

[Flags]
public enum CustomerInclude
{
    None = 0,
    Addresses = 1 << 0,
}