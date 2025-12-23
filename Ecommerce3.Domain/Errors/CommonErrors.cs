namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class Common
    {
        public static readonly DomainError Null =
            new("Common.Null", "Value cannot be null.");

        public static readonly DomainError Empty =
            new("Common.Empty", "Value cannot be empty.");

        public static readonly DomainError Invalid =
            new("Common.Invalid", "Value is invalid.");

        public static readonly DomainError NotFound =
            new("Common.NotFound", "Requested entity was not found.");

        public static readonly DomainError AlreadyExists =
            new("Common.AlreadyExists", "Entity already exists.");

        public static readonly DomainError TooLong =
            new("Common.TooLong", "Value exceeds maximum allowed length.");

        public static readonly DomainError TooShort =
            new("Common.TooShort", "Value is shorter than the minimum allowed length.");

        public static readonly DomainError OutOfRange =
            new("Common.OutOfRange", "Value is outside the allowed range.");

        public static readonly DomainError Unauthorized =
            new("Common.Unauthorized", "You are not authorized to perform this action.");

        public static readonly DomainError Forbidden =
            new("Common.Forbidden", "This operation is not permitted.");

        public static readonly DomainError Conflict =
            new("Common.Conflict", "Operation conflicts with the current state.");

        public static readonly DomainError Required =
            new("Common.Required", "A required value is missing.");

        public static readonly DomainError InvalidFormat =
            new("Common.InvalidFormat", "Value has an invalid format.");

        public static readonly DomainError InvalidState =
            new("Common.InvalidState", "The entity is in an invalid state for this operation.");
        
        public static readonly DomainError GenericErrorMessage =
            new("Common.InvalidState", "An unexpected error occurred. Please check the errors.");
    }
}
