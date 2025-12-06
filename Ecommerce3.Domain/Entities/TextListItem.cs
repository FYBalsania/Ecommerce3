using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public abstract class TextListItem : Entity, ICreatable, IUpdatable, IDeletable
{
    public static readonly int TextMaxLength = 255;

    public TextListItemType Type { get; private set; }
    public string Text { get; private set; }
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

    private protected TextListItem() : base()
    {
    }

    protected TextListItem(TextListItemType type, string text, decimal sortOrder, int createdBy, DateTime createdAt,
        string createdByIp)
    {
        ValidateText(text);
        ValidateCreatedBy(createdBy);
        ValidateCreatedByIp(createdByIp);

        Type = type;
        Text = text;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    public static TextListItem Create(Type parentEntity, int parentEntityId, TextListItemType type, string text,
        decimal sortOrder, int createdBy, DateTime createdAt, string createdByIp)
    {
        return parentEntity == typeof(ProductTextListItem)
            ? new ProductTextListItem(parentEntityId, type, text, sortOrder, createdBy, createdAt, createdByIp)
            : throw new NotImplementedException("Create method not implemented for this entity type.");
    }

    public bool Update(string text, decimal sortOrder, int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        ValidateText(text);
        ValidateUpdatedBy(updatedBy);
        ValidateUpdatedByIp(updatedByIp);

        if (Text == text && SortOrder == sortOrder) return false;

        Text = text;
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

    private static void ValidateText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new DomainException(DomainErrors.TextListItemErrors.TextRequired);
    }

    private static void ValidateCreatedBy(int createdBy)
    {
        if (createdBy <= 0) throw new DomainException(DomainErrors.TextListItemErrors.InvalidCreatedBy);
    }

    private static void ValidateCreatedByIp(string createdByIp)
    {
        if (string.IsNullOrWhiteSpace(createdByIp))
            throw new DomainException(DomainErrors.TextListItemErrors.CreatedByIpRequired);
        if (createdByIp.Length > ICreatable.CreatedByIpMaxLength)
            throw new DomainException(DomainErrors.TextListItemErrors.CreatedByIpTooLong);
    }

    private static void ValidateUpdatedBy(int updatedBy)
    {
        if (updatedBy <= 0) throw new DomainException(DomainErrors.TextListItemErrors.InvalidUpdatedBy);
    }

    private static void ValidateUpdatedByIp(string updatedByIp)
    {
        if (string.IsNullOrWhiteSpace(updatedByIp))
            throw new DomainException(DomainErrors.TextListItemErrors.UpdatedByIpRequired);
        if (updatedByIp.Length > IUpdatable.UpdatedByIpMaxLength)
            throw new DomainException(DomainErrors.TextListItemErrors.UpdatedByIpTooLong);
    }

    private static void ValidateDeletedBy(int deletedBy)
    {
        if (deletedBy <= 0) throw new DomainException(DomainErrors.TextListItemErrors.InvalidDeletedBy);
    }

    private static void ValidateDeletedByIp(string deletedByIp)
    {
        if (string.IsNullOrWhiteSpace(deletedByIp))
            throw new DomainException(DomainErrors.TextListItemErrors.DeletedByIpRequired);
        if (deletedByIp.Length > IDeletable.DeletedByIpMaxLength)
            throw new DomainException(DomainErrors.TextListItemErrors.DeletedByIpTooLong);
    }
}