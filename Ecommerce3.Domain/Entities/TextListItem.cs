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
        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.TextListItemErrors.InvalidCreatedBy);
        ICreatable.ValidateCreatedByIp(createdByIp, DomainErrors.TextListItemErrors.CreatedByIpRequired, DomainErrors.TextListItemErrors.CreatedByIpTooLong);

        Type = type;
        Text = text;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    public static TextListItem Create(Type entity, int parentEntityId, TextListItemType type, string text,
        decimal sortOrder, int createdBy, DateTime createdAt, string createdByIp)
    {
        return entity == typeof(ProductTextListItem)
            ? new ProductTextListItem(parentEntityId, type, text, sortOrder, createdBy, createdAt, createdByIp)
            : throw new NotImplementedException("Create method not implemented for this entity type.");
    }

    public void Update(string text, decimal sortOrder, int updatedBy, DateTime updatedAt, string updatedByIp)
    {
        ValidateText(text);
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.TextListItemErrors.InvalidUpdatedBy);
        IUpdatable.ValidateUpdatedByIp(updatedByIp, DomainErrors.TextListItemErrors.UpdatedByIpRequired, DomainErrors.TextListItemErrors.UpdatedByIpTooLong);

        if (Text == text && SortOrder == sortOrder) return;

        Text = text;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = updatedAt;
        UpdatedByIp = updatedByIp;
    }

    public void Delete(int deletedBy, DateTime deletedAt, string deletedByIp)
    {
        IDeletable.ValidateDeletedBy(deletedBy, DomainErrors.TextListItemErrors.InvalidDeletedBy);
        IDeletable.ValidateDeletedByIp(deletedByIp, DomainErrors.TextListItemErrors.DeletedByIpRequired, DomainErrors.TextListItemErrors.DeletedByIpTooLong);

        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }

    private static void ValidateText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new DomainException(DomainErrors.TextListItemErrors.TextRequired);
    }
}