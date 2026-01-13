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
}