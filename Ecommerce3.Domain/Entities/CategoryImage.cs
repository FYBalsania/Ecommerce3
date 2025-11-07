using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class CategoryImage : Image
{
    public int CategoryId { get; private set; }
    public Category? Category { get; private set; }
    
    private CategoryImage() : base()
    {
    }

    internal CategoryImage(string ogFileName, string fileName, string fileExtension, int imageTypeId, ImageSize size,
        string? altText, string? title, string loading, string? link, string? linkTarget, int categoryId, int sortOrder,
        int createdBy, DateTime createdAt, string createdByIp)
        : base(ogFileName, fileName, fileExtension, imageTypeId, size, altText, title, loading, link, linkTarget,
            sortOrder, createdBy, createdAt, createdByIp)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(categoryId, 0, nameof(categoryId));
        CategoryId = categoryId;
    }
}