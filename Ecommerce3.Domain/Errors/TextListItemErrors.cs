using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class TextListItemErrors
    {
        public static readonly DomainError ParentEntityRequired =
            new($"{nameof(TextListItem)}.ParentEntity", "Parent entity is required.");
        
        public static readonly DomainError EntityRequired =
            new($"{nameof(TextListItem)}.Entity", "Entity is required.");
        
        public static readonly DomainError InvalidId =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.Id)}", "Id is invalid.");
        
        public static readonly DomainError TextRequired =
            new DomainError($"{nameof(TextListItem)}.{nameof(TextListItem.Text)}", "Text is required.");
        
        public static readonly DomainError DuplicateText =
            new DomainError($"{nameof(TextListItem)}.{nameof(TextListItem.Text)}", "Duplicate text.");
        
        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.UpdatedBy)}", "Updated by is invalid.");
        
        public static readonly DomainError InvalidDeletedBy =
            new($"{nameof(TextListItem)}.{nameof(TextListItem.DeletedBy)}", "Deleted by is invalid.");
    }
}