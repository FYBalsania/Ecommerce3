using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class PageImage : Image
{
    public int PageId { get; private set; }
    public Page? Page { get; private set; }
    
    private PageImage() : base()
    {
    }
    
    internal PageImage(string ogFileName, string fileName, string fileExtension, int imageTypeId, ImageSize size,
        string? altText, string? title, string loading, string? link, string? linkTarget, int pageId, int sortOrder,
        int createdBy, DateTime createdAt, string createdByIp)
        : base(ogFileName, fileName, fileExtension, imageTypeId, size, altText, title, loading, link, linkTarget,
            sortOrder, createdBy, createdAt, createdByIp)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(pageId, 0, nameof(pageId));
        PageId = pageId;
    }
}