namespace Ecommerce3.Domain.Exceptions;

[Serializable]
public class OperationNotAllowedException : DomainException
{
    public OperationNotAllowedException(string message)
        : base(message, "OPERATION_NOT_ALLOWED") { }
}
