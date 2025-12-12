using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public interface ICreatable
{
    static readonly int CreatedByIpMaxLength = 128;

    int CreatedBy { get; }
    DateTime CreatedAt { get; }
    string CreatedByIp { get; }

    public static void ValidateCreatedBy(int createdBy, DomainError domainError)
    {
        if (createdBy <= 0) throw new DomainException(domainError);
    }

    public static void ValidateCreatedByIp(string createdByIp, DomainError requiredDomainError,
        DomainError tooLongDomainError)
    {
        if (string.IsNullOrWhiteSpace(createdByIp)) throw new DomainException(requiredDomainError);
        if (createdByIp.Length > CreatedByIpMaxLength) throw new DomainException(tooLongDomainError);
    }
}