namespace Ecommerce3.Domain.Exceptions;

[Serializable]
public class AggregateStateException : DomainException
{
    public AggregateStateException(string message)
        : base(message, "AGGREGATE_STATE_INVALID") { }
}
