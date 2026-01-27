using Ecommerce3.Domain.Errors;

namespace Ecommerce3.Domain.Exceptions;

[Serializable]
public class DomainException(DomainError error) : Exception(error.Message)
{
    public DomainError Error { get; } = error;
}