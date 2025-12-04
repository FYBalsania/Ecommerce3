namespace Ecommerce3.Domain.Entities;

public interface IUpdatable
{
    static readonly int UpdatedByIpMaxLength = 128;
    
    int? UpdatedBy { get; }
    DateTime? UpdatedAt { get; }
    string? UpdatedByIp { get; }
}