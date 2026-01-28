using System.Net;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public interface IDeletable
{
    static readonly int DeletedByIpMaxLength = 128;

    int? DeletedBy { get; }
    DateTime? DeletedAt { get; }
    IPAddress? DeletedByIp { get; }

    public static void ValidateDeletedBy(int deletedBy, DomainError domainError)
    {
        if (deletedBy <= 0) throw new DomainException(domainError);
    }
    
    public static void ValidateDeletedByIp(IPAddress deletedByIp, DomainError domainError)
    {
        if (deletedByIp == null) throw new DomainException(domainError);
    }
    
    public static void ValidateDeletedAt(DateTime deletedAt, DomainError domainError)
    {
        if (deletedAt == default) throw new DomainException(domainError);
    }
}