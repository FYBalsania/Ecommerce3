namespace Ecommerce3.Domain.Entities;

public interface IDeletable
{
    static readonly int DeletedByIpMaxLength = 128;
    
    int? DeletedBy { get; }
    DateTime? DeletedAt { get; }
    string? DeletedByIp { get; }
}