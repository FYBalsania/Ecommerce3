using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class PostCodeErrors
    {
        public static readonly DomainError InvalidId =
            new($"{nameof(PostCode)}.{nameof(PostCode.Id)}", "Post code id is invalid.");
        
        public static readonly DomainError CodeRequired =
            new($"{nameof(PostCode)}.{nameof(PostCode.Code)}", "Code is required.");

        public static readonly DomainError CodeTooLong =
            new($"{nameof(PostCode)}.{nameof(PostCode.Code)}", "Code cannot exceed 16 characters.");
        
        public static readonly DomainError DuplicateCode =
            new($"{nameof(PostCode)}.{nameof(PostCode.Code)}", "Duplicate code.");
        
        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(PostCode)}.{nameof(PostCode.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(PostCode)}.{nameof(PostCode.UpdatedBy)}", "Updated by is invalid.");
    }
}