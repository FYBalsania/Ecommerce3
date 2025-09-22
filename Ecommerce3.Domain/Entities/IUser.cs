namespace Ecommerce3.Domain.Entities;

public interface IUser : ICreatable, IUpdatable
{
    string FirstName { get; }
    string? LastName { get; }
    string? FullName => FirstName + (LastName is not null ? " " + LastName : "");
}