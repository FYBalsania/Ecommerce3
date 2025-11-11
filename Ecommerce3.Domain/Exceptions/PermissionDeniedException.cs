namespace Ecommerce3.Domain.Exceptions;

[Serializable]
public class PermissionDeniedException : DomainException
{
    public PermissionDeniedException(string message)
        : base(message, "PERMISSION_DENIED") { }
}
