using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Exceptions;

public class DuplicateException: Exception
{
    public string ParamName { get; }

    public DuplicateException(string message, string paramName) : base(message)
    {
        ParamName = paramName;
    }
}