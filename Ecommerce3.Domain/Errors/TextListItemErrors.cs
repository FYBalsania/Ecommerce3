using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class TextListItemErrors
    {
        public static readonly DomainError InvalidId =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.Id)}", "Id is invalid.");
        
        public static readonly DomainError TextRequired =
            new DomainError($"{nameof(TextListItem)}.{nameof(TextListItem.Text)}", "Text is required.");
        
        public static readonly DomainError DuplicateText =
            new DomainError($"{nameof(TextListItem)}.{nameof(TextListItem.Text)}", "Duplicate text.");
        
        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.CreatedByIp)}", "Created by IP address is required.");

        public static readonly DomainError CreatedByIpTooLong =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.CreatedByIp)}",
                $"Created by IP address cannot exceed {ICreatable.CreatedByIpMaxLength} characters.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.UpdatedBy)}", "Updated by is invalid.");

        public static readonly DomainError UpdatedByIpRequired =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.UpdatedByIp)}", "Updated by IP address is required.");

        public static readonly DomainError UpdatedByIpTooLong =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.UpdatedByIp)}",
                $"Updated by IP address cannot exceed {IUpdatable.UpdatedByIpMaxLength} characters.");
        
        public static readonly DomainError InvalidDeletedBy =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.DeletedBy)}", "Deleted by is invalid.");

        public static readonly DomainError DeletedByIpRequired =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.DeletedByIp)}", "Deleted by IP address is required.");

        public static readonly DomainError DeletedByIpTooLong =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.DeletedByIp)}",
                $"Updated by IP address cannot exceed {IDeletable.DeletedByIpMaxLength} characters.");
    }
}