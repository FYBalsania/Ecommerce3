namespace Ecommerce3.Domain.Enums;

[Flags]
public enum UnitOfMeasureInclude
{
    None = 0,
    Base = 1 << 0,
    CreatedUser = 1 << 1,
    UpdatedUser = 1 << 2,
    DeletedUser = 1 << 3,
}