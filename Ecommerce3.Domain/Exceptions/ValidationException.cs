namespace Ecommerce3.Domain.Exceptions;

[Serializable]
public class ValidationException : DomainException
{
    public IReadOnlyDictionary<string, string[]> Errors { get; }

    public ValidationException(Dictionary<string, string[]> errors)
        : base("Validation failed for one or more domain properties.", "VALIDATION_ERROR")
    {
        Errors = errors;
    }
}
