namespace Ecommerce3.Domain.Entities;

public abstract class EntityWithImages : Entity
{
    private readonly List<Image> _images = [];
    public IReadOnlyList<Image> Images => _images;
}