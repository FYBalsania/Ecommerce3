using Ecommerce3.Domain.Errors;

namespace Ecommerce3.Domain.Exceptions;

[Serializable]
public class DomainException : Exception
{
    public DomainError Error { get; }

    public DomainException(DomainError error) : base(error.Message)
    {
        Error = error;
    }
}