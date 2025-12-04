namespace Ecommerce3.Domain.Entities;

public interface ICreatable
{
    static readonly int CreatedByIpMaxLength = 128;
    
    int CreatedBy { get; }
    DateTime CreatedAt { get; }
    string CreatedByIp { get; }
}