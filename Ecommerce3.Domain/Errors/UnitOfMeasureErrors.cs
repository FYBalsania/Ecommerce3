using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class UnitOfMeasureErrors
    {
        public static readonly DomainError InvalidUnitOfMeasureId =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.Id)}", "Unit of measure ID is invalid.");
        
        public static readonly DomainError CodeRequired =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.Code)}", "Code is required.");

        public static readonly DomainError CodeTooLong =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.Code)}",
                $"Code cannot exceed {UnitOfMeasure.CodeMaxLength} characters.");

        public static readonly DomainError DuplicateCode =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.Code)}", "Duplicate code.");

        public static readonly DomainError NameRequired =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.Name)}", "Name is required.");

        public static readonly DomainError NameTooLong =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.Name)}",
                $"Name cannot exceed {UnitOfMeasure.NameMaxLength} characters.");

        public static readonly DomainError DuplicateName =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.Name)}", "Duplicate name.");

        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError CreatedByIpRequired =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.CreatedByIp)}", "Created by IP address is required.");

        public static readonly DomainError CreatedByIpTooLong =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.CreatedByIp)}",
                $"Created by IP address cannot exceed {ICreatable.CreatedByIpMaxLength} characters.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.UpdatedBy)}", "Updated by is invalid.");

        public static readonly DomainError UpdatedByIpRequired =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.UpdatedByIp)}", "Updated by IP address is required.");

        public static readonly DomainError UpdatedByIpTooLong =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.UpdatedByIp)}",
                $"Updated by IP address cannot exceed {IUpdatable.UpdatedByIpMaxLength} characters.");
        
        public static readonly DomainError InvalidDeletedBy =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.DeletedBy)}", "Deleted by is invalid.");

        public static readonly DomainError DeletedByIpRequired =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.DeletedByIp)}", "Deleted by IP address is required.");

        public static readonly DomainError DeletedByIpTooLong =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.DeletedByIp)}",
                $"Updated by IP address cannot exceed {IDeletable.DeletedByIpMaxLength} characters.");
    }
}