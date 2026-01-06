using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public abstract class KVPListItem : Entity, ICreatable, IUpdatable, IDeletable
{
    public KVPListItemType Type { get; private set; }
    public string Key { get; private set; }
    public string Value { get; private set; }
    public decimal SortOrder { get; private set; }
    public int CreatedBy { get; private set; }
    public IAppUser? CreatedByUser { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }

    private protected KVPListItem()
    {
    }

    private protected KVPListItem(KVPListItemType type, string key, string value, decimal sortOrder, int createdBy,
        DateTime createdAt, string createdByIp)
    {
        ValidateKey(key);
        ValidateValue(value);
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.KVPListItemErrors.InvalidCreatedBy);
        ICreatable.ValidateCreatedByIp(createdByIp, DomainErrors.KVPListItemErrors.CreatedByIpRequired, 
            DomainErrors.KVPListItemErrors.CreatedByIpTooLong);

        Type = type;
        Key = key;
        Value = value;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    public void Update(string key, string value, decimal sortOrder, int updatedBy, DateTime updatedAt,
        string updatedByIp)
    {
        ValidateKey(key);
        ValidateValue(value);
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.KVPListItemErrors.InvalidUpdatedBy);
        IUpdatable.ValidateUpdatedByIp(updatedByIp, DomainErrors.KVPListItemErrors.UpdatedByIpRequired, 
            DomainErrors.KVPListItemErrors.UpdatedByIpTooLong);

        if (Key == key && Value == value && SortOrder == sortOrder)
            return;

        Key = key;
        Value = value;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;
    }

    public void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        IDeletable.ValidateDeletedBy(deletedBy, DomainErrors.KVPListItemErrors.InvalidDeletedBy);
        IDeletable.ValidateDeletedByIp(deletedByIp, DomainErrors.KVPListItemErrors.DeletedByIpRequired, 
            DomainErrors.KVPListItemErrors.DeletedByIpTooLong);

        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }

    public static KVPListItem Create(Type parentEntityType, int parentEntityId, KVPListItemType type, string key,
        string value, decimal sortOrder, int createdBy, DateTime createdAt, string createdByIp)
    {
        if (parentEntityType == typeof(Product))
            return new ProductKVPListItem(type, key, value, sortOrder, parentEntityId, createdBy, createdAt,
                createdByIp);

        if (parentEntityType == typeof(Category))
            return new CategoryKVPListItem(type, key, value, sortOrder, parentEntityId, createdBy, createdAt,
                createdByIp);

        throw new ArgumentException("Invalid parent entity type", nameof(parentEntityType));
    }

    private static void ValidateKey(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
            throw new DomainException(DomainErrors.KVPListItemErrors.KeyRequired);
    }

    private static void ValidateValue(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(DomainErrors.KVPListItemErrors.ValueRequired);
    }
}