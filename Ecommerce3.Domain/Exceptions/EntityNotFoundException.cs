namespace Ecommerce3.Domain.Exceptions;

[Serializable]
public class EntityNotFoundException : DomainException
{
    public string EntityName { get; }
    public int Id { get; }

    public EntityNotFoundException(string entityName, int key)
        : base($"{entityName} not found.", $"{entityName.ToUpperInvariant()}_NOT_FOUND")
    {
        EntityName = entityName;
        Id = key;
    }
}