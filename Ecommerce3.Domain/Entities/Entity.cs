using Ecommerce3.Domain.DomainEvents;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public abstract class Entity
{
    public int Id { get; private set; }

    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();

    protected static void ValidateRequiredAndTooLong(string value, int maxLength, DomainError requiredDomainError,
        DomainError tooLongDomainError)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(requiredDomainError);
        if (value.Length > maxLength) throw new DomainException(tooLongDomainError);
    }

    protected static void ValidateTooLong(string value, int maxLength, DomainError tooLongDomainError)
    {
        if (value.Length > maxLength) throw new DomainException(tooLongDomainError);
    }

    protected static void ValidatePositiveNumber(int id, DomainError domainError)
    {
        if (id <= 0) throw new DomainException(domainError);
    }

    protected static void ValidatePositiveNumber(decimal value, DomainError domainError)
    {
        if (value <= 0) throw new DomainException(domainError);
    }

    protected static void ValidatePositiveAndZeroNumber(decimal value, DomainError domainError)
    {
        if (value < 0) throw new DomainException(domainError);
    }

    protected static void ValidateUrl(string url, UriKind uriKind, DomainError domainError)
    {
        if (!Uri.TryCreate(url, uriKind, out _))
            throw new DomainException(domainError);
    }
}