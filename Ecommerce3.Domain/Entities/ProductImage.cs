using System.Net;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class ProductImage : Image
{
    public int ProductId { get; private set; }
    public Product? Product { get; private set; }

    private ProductImage() : base()
    {
    }

    internal ProductImage(string ogFileName, string fileName, string fileExtension, int imageTypeId, ImageSize size,
        string? altText, string? title, ImageLoading loading, string? link, string? linkTarget, int productId,
        int sortOrder, int createdBy, DateTime createdAt, IPAddress createdByIp)
        : base(ogFileName, fileName, fileExtension, imageTypeId, size, altText, title, loading, link, linkTarget,
            sortOrder, createdBy, createdAt, createdByIp)
    {
        if (productId <= 0) throw new DomainException(DomainErrors.ImageErrors.InvalidProductId);
        ProductId = productId;
    }
}