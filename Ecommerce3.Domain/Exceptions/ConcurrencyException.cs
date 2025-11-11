namespace Ecommerce3.Domain.Exceptions;

[Serializable]
public class ConcurrencyException : DomainException
{
    public ConcurrencyException(string message = "Concurrency conflict detected.")
        : base(message, "CONCURRENCY_CONFLICT") { }
}
