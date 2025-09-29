namespace Ecommerce3.Domain.Enums;

[Flags]
public enum CustomerInclude
{
    Addresses = 1 << 0,
}