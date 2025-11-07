using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductImage : Image
{
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }

    private ProductImage() : base()
    {
    }
    
    internal ProductImage(string ogFileName, string fileName, string fileExtension, int imageTypeId, ImageSize size,
        string? altText, string? title, string loading, string? link, string? linkTarget, int productId, int sortOrder,
        int createdBy, DateTime createdAt, string createdByIp)
        : base(ogFileName, fileName, fileExtension, imageTypeId, size, altText, title, loading, link, linkTarget,
            sortOrder, createdBy, createdAt, createdByIp)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(productId, 0, nameof(productId));
        ProductId = productId;
    }
}