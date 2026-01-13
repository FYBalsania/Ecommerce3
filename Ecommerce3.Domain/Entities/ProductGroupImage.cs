using System.Net;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductGroupImage : Image
{
    public int ProductGroupId { get; private set; }
    public ProductGroup? ProductGroup { get; private set; }

    private ProductGroupImage() : base()
    {
    }

    internal ProductGroupImage(string ogFileName, string fileName, string fileExtension, int imageTypeId,
        ImageSize size, string? altText, string? title, ImageLoading loading, string? link, string? linkTarget,
        int productGroupId, int sortOrder, int createdBy, DateTime createdAt, IPAddress createdByIp)
        : base(ogFileName, fileName, fileExtension, imageTypeId, size, altText, title, loading, link, linkTarget,
            sortOrder, createdBy, createdAt, createdByIp)
    {
        if (productGroupId <= 0) throw new DomainException(DomainErrors.ImageErrors.InvalidProductGroupId);
        ProductGroupId = productGroupId;
    }
}