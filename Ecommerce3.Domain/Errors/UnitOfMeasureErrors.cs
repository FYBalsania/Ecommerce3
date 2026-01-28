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
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.SingularName)}", "Name is required.");

        public static readonly DomainError NameTooLong =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.SingularName)}",
                $"Name cannot exceed {UnitOfMeasure.NameMaxLength} characters.");

        public static readonly DomainError DuplicateName =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.SingularName)}", "Duplicate name.");

        public static readonly DomainError InvalidCreatedBy =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.CreatedBy)}", "Created by is invalid.");

        public static readonly DomainError InvalidUpdatedBy =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.UpdatedBy)}", "Updated by is invalid.");
        
        public static readonly DomainError InvalidDeletedBy =
            new($"{nameof(UnitOfMeasure)}.{nameof(UnitOfMeasure.DeletedBy)}", "Deleted by is invalid.");
    }
}