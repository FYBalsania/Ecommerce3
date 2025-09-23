namespace Ecommerce3.Domain.Entities;

public interface IAppUser : ICreatable, IUpdatable
{
    string FirstName { get; }
    string? LastName { get; }
    string FullName { get; }
}