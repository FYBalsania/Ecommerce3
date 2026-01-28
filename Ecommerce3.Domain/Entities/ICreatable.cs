using System.Net;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public interface ICreatable
{
    static readonly int CreatedByIpMaxLength = 128;

    int CreatedBy { get; }
    DateTime CreatedAt { get; }
    IPAddress CreatedByIp { get; }

    public static void ValidateCreatedBy(int createdBy, DomainError domainError)
    {
        if (createdBy <= 0) throw new DomainException(domainError);
    }
    
    public static void ValidateCreatedByIp(IPAddress createdByIp, DomainError domainError)
    {
        if (createdByIp == null) throw new DomainException(domainError);
    }
    
    public static void ValidateCreatedAt(DateTime createdAt, DomainError domainError)
    {
        if (createdAt == default) throw new DomainException(domainError);
    }
}