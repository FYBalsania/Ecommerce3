using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class BankErrors
    {
        public static readonly DomainError NameRequired =
            new($"{nameof(Bank)}.{nameof(Bank.Name)}", "Name is required.");

        public static readonly DomainError NameTooLong =
            new($"{nameof(Bank)}.{nameof(Bank.Name)}", "Name cannot exceed 256 characters.");

        public static readonly DomainError DuplicateName =
            new($"{nameof(Bank)}.{nameof(Bank.Name)}", "Duplicate name.");

        public static readonly DomainError SlugRequired =
            new($"{nameof(Bank)}.{nameof(Bank.Slug)}", "Slug is required.");
        
        public static readonly DomainError DuplicateSlug =
            new($"{nameof(Bank)}.{nameof(Bank.Slug)}", "Duplicate slug.");

        public static readonly DomainError SlugTooLong =
            new($"{nameof(Bank)}.{nameof(Bank.Slug)}", "Slug cannot exceed 256 characters.");
        
        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(Bank)}.{nameof(Bank.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(Bank)}.{nameof(Bank.CreatedByIp)}", "Created by IP address is required.");

        public static readonly DomainError CreatedByIpTooLong =
            new($"{nameof(Bank)}.{nameof(Bank.CreatedByIp)}", "Created by IP address cannot exceed 128 characters.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(Bank)}.{nameof(Bank.UpdatedBy)}", "Updated by is invalid.");

        public static readonly DomainError UpdatedByIpRequired =
            new($"{nameof(Bank)}.{nameof(Bank.UpdatedByIp)}", "Updated by IP address is required.");

        public static readonly DomainError UpdatedByIpTooLong =
            new($"{nameof(Bank)}.{nameof(Bank.UpdatedByIp)}", "Updated by IP address cannot exceed 128 characters.");
    }
}