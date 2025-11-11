namespace Ecommerce3.Domain.Exceptions;

[Serializable]
public class BusinessRuleViolationException : DomainException
{
    public BusinessRuleViolationException(string message, string errorCode) : base(message, errorCode)
    {
    }
}