using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;

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
        ValidateCreatedBy(createdBy);
        ValidateCreatedByIp(createdByIp);

        Type = type;
        Key = key;
        Value = value;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    public bool Update(string key, string value, decimal sortOrder, int updatedBy, DateTime updatedAt,
        string updatedByIp)
    {
        ValidateKey(key);
        ValidateValue(value);
        ValidateUpdatedBy(updatedBy);
        ValidateUpdatedByIp(updatedByIp);

        if (Key == key && Value == value && SortOrder == sortOrder)
            return false;

        Key = key;
        Value = value;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;

        return true;
    }

    public void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        ValidateDeletedBy(deletedBy);
        ValidateDeletedByIp(deletedByIp);

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

    private static void ValidateCreatedBy(int createdBy)
    {
        if (createdBy <= 0) throw new DomainException(DomainErrors.KVPListItemErrors.InvalidCreatedBy);
    }

    private static void ValidateCreatedByIp(string createdByIp)
    {
        if (string.IsNullOrWhiteSpace(createdByIp))
            throw new DomainException(DomainErrors.KVPListItemErrors.CreatedByIpRequired);
        if (createdByIp.Length > ICreatable.CreatedByIpMaxLength)
            throw new DomainException(DomainErrors.KVPListItemErrors.CreatedByIpTooLong);
    }

    private static void ValidateUpdatedBy(int updatedBy)
    {
        if (updatedBy <= 0) throw new DomainException(DomainErrors.KVPListItemErrors.InvalidUpdatedBy);
    }

    private static void ValidateUpdatedByIp(string updatedByIp)
    {
        if (string.IsNullOrWhiteSpace(updatedByIp))
            throw new DomainException(DomainErrors.KVPListItemErrors.UpdatedByIpRequired);
        if (updatedByIp.Length > IUpdatable.UpdatedByIpMaxLength)
            throw new DomainException(DomainErrors.KVPListItemErrors.UpdatedByIpTooLong);
    }

    private static void ValidateDeletedBy(int deletedBy)
    {
        if (deletedBy <= 0) throw new DomainException(DomainErrors.KVPListItemErrors.InvalidDeletedBy);
    }

    private static void ValidateDeletedByIp(string deletedByIp)
    {
        if (string.IsNullOrWhiteSpace(deletedByIp))
            throw new DomainException(DomainErrors.KVPListItemErrors.DeletedByIpRequired);
        if (deletedByIp.Length > IDeletable.DeletedByIpMaxLength)
            throw new DomainException(DomainErrors.KVPListItemErrors.DeletedByIpTooLong);
    }
}