using System.Net;
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
    public IPAddress CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public IPAddress? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public IPAddress? DeletedByIp { get; private set; }

    private protected KVPListItem()
    {
    }

    private protected KVPListItem(KVPListItemType type, string key, string value, decimal sortOrder, int createdBy,
        DateTime createdAt, IPAddress createdByIp)
    {
        ValidateKey(key);
        ValidateValue(value);
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.KVPListItemErrors.InvalidCreatedBy);

        Type = type;
        Key = key;
        Value = value;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    public void Update(string key, string value, decimal sortOrder, int updatedBy, DateTime updatedAt, IPAddress updatedByIp)
    {
        ValidateKey(key);
        ValidateValue(value);
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.KVPListItemErrors.InvalidUpdatedBy);

        if (Key == key && Value == value && SortOrder == sortOrder)
            return;

        Key = key;
        Value = value;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;
    }

    public void Delete(int deletedBy, DateTime deletedAt, IPAddress deletedByIp)
    {
        IDeletable.ValidateDeletedBy(deletedBy, DomainErrors.KVPListItemErrors.InvalidDeletedBy);

        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }

    public static KVPListItem Create(Type parentEntityType, int parentEntityId, KVPListItemType type, string key,
        string value, decimal sortOrder, int createdBy, DateTime createdAt, IPAddress createdByIp)
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