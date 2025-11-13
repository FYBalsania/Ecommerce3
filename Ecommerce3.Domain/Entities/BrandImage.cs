using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class BrandImage : Image
{
    public int BrandId { get; private set; }
    public Brand? Brand { get; private set; }

    private BrandImage() : base()
    {
    }

    internal BrandImage(string ogFileName, string fileName, string fileExtension, int imageTypeId, ImageSize size,
        string? altText, string? title, ImageLoading loading, string? link, string? linkTarget, int brandId, int sortOrder,
        int createdBy, DateTime createdAt, string createdByIp)
        : base(ogFileName, fileName, fileExtension, imageTypeId, size, altText, title, loading, link, linkTarget,
            sortOrder, createdBy, createdAt, createdByIp)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(brandId, 0, nameof(brandId));
        BrandId = brandId;
    }
}