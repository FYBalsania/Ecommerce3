namespace Ecommerce3.Domain.Entities;

public interface IUpdatable
{
    int? UpdatedBy { get; }
    DateTime? UpdatedAt { get; }
    string? UpdatedByIp { get; }
}