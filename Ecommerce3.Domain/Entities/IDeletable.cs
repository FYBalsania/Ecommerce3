namespace Ecommerce3.Domain.Entities;

public interface IDeletable
{
    int? DeletedBy { get; }
    DateTime? DeletedAt { get; }
    string? DeletedByIp { get; }
}