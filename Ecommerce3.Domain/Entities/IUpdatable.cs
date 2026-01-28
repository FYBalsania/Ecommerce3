using System.Net;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public interface IUpdatable
{
    static readonly int UpdatedByIpMaxLength = 128;

    int? UpdatedBy { get; }
    DateTime? UpdatedAt { get; }
    IPAddress? UpdatedByIp { get; }

    public static void ValidateUpdatedBy(int updatedBy, DomainError domainError)
    {
        if (updatedBy <= 0) throw new DomainException(domainError);
    }
    
    public static void ValidateUpdatedByIp(IPAddress updatedByIp, DomainError domainError)
    {
        if (updatedByIp == null) throw new DomainException(domainError);
    }
    
    public static void ValidateUpdatedAt(DateTime updatedAt, DomainError domainError)
    {
        if (updatedAt == default) throw new DomainException(domainError);
    }
}