namespace Ecommerce3.Domain.Entities;

public interface ICreatable
{
    int CreatedBy { get; }
    DateTime CreatedAt { get; }
    string CreatedByIp { get; }
}