namespace Ecommerce3.Domain.Enums;

[Flags]
public enum BankInclude
{
    None = 0,
    CreatedUser = 1 << 0,
    UpdatedUser = 1 << 1,
    DeletedUser = 1 << 2,
}