namespace Ecommerce3.Domain.Entities;

public abstract class EntityWithImages<T> : Entity where T : Image
{
    protected readonly List<T> _images = [];
    public abstract string ImageNamePrefix { get; }
    public IReadOnlyList<T> Images => _images;
}