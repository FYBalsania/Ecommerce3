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

        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(PostCode)}.{nameof(PostCode.CreatedByIp)}", "Created by IP address is required.");

        public static readonly DomainError CreatedByIpTooLong =
            new($"{nameof(PostCode)}.{nameof(PostCode.CreatedByIp)}",
                $"Created by IP address cannot exceed {ICreatable.CreatedByIpMaxLength} characters.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(PostCode)}.{nameof(PostCode.UpdatedBy)}", "Updated by is invalid.");

        public static readonly DomainError UpdatedByIpRequired =
            new($"{nameof(PostCode)}.{nameof(PostCode.UpdatedByIp)}", "Updated by IP address is required.");

        public static readonly DomainError UpdatedByIpTooLong =
            new($"{nameof(PostCode)}.{nameof(PostCode.UpdatedByIp)}",
                $"Updated by IP address cannot exceed {IUpdatable.UpdatedByIpMaxLength} characters.");
    }
}