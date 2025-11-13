using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductGroupImage : Image
{
    public int ProductGroupId { get; private set; }
    public ProductGroup? ProductGroup { get; private set; }

    private ProductGroupImage() : base()
    {
    }
    
    internal ProductGroupImage(string ogFileName, string fileName, string fileExtension, int imageTypeId, ImageSize size,
        string? altText, string? title, ImageLoading loading, string? link, string? linkTarget, int productGroupId,
        int sortOrder, int createdBy, DateTime createdAt, string createdByIp)
        : base(ogFileName, fileName, fileExtension, imageTypeId, size, altText, title, loading, link, linkTarget,
            sortOrder, createdBy, createdAt, createdByIp)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(productGroupId, 0, nameof(productGroupId));
        ProductGroupId = productGroupId;
    }
}