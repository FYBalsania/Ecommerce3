namespace Ecommerce3.Domain.Enums;

[Flags]
public enum CategoryInclude
{
    Images = 1 << 0,
    Parent = 1 << 1,
    CreatedByUser = 1 << 2,
    UpdatedByUser = 1 << 3,
    DeletedByUser = 1 << 4,
    KVPListItems = 1 << 5,
}