namespace Ecommerce3.Domain.Stubs;

public abstract class Entity
{
    public int Id { get; private set; }
}

public class Image : Entity
{
    public string? FileName { get; private set; }
}

public class BrandImage : Image
{
    public int BrandId { get; private set; }
}

public class CategoryImage : Image
{
    public int CategoryId { get; private set; }
}

public class ProductImage : Image
{
    public int ProductId { get; private set; }
}

public abstract class EntityWithImages<T> : Entity where T : Image
{
    protected readonly List<T> _images = [];
    public IReadOnlyList<T> Images => _images;
}

public class Brand : EntityWithImages<BrandImage>
{
    public string Name { get; private set; }
}

public class Category: EntityWithImages<CategoryImage>
{
    public string Name { get; private set; }
}

public class Product: EntityWithImages<ProductImage>
{
    public string SKU { get; private set; }
    public string Name { get; private set; }
}