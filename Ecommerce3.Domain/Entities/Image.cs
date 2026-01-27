using System.Net;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public class Image : Entity, ICreatable, IUpdatable, IDeletable
{
    public string OgFileName { get; private set; }
    public string FileName { get; private set; }
    public string FileExtension { get; private set; }
    public int ImageTypeId { get; private set; }
    public ImageType? ImageType { get; private set; }
    public ImageSize Size { get; private set; }
    public string? AltText { get; private set; }
    public string? Title { get; private set; }
    public ImageLoading Loading { get; private set; }
    public string? Link { get; private set; }
    public string? LinkTarget { get; private set; }
    public int SortOrder { get; private set; }
    public int CreatedBy { get; private set; }
    public IAppUser? CreatedByUser { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public IPAddress CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public IPAddress? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public IPAddress? DeletedByIp { get; private set; }

    private protected Image()
    {
    }

    internal Image(string ogFileName, string fileName, string fileExtension, int imageTypeId, ImageSize size,
        string? altText, string? title, ImageLoading loading, string? link, string? linkTarget, int sortOrder,
        int createdBy, DateTime createdAt, IPAddress createdByIp)
    {
        if (string.IsNullOrWhiteSpace(ogFileName))
            throw new DomainException(DomainErrors.ImageErrors.OriginalFileNameRequired);

        if (string.IsNullOrWhiteSpace(fileName))
            throw new DomainException(DomainErrors.ImageErrors.FileNameRequired);

        if (string.IsNullOrWhiteSpace(fileExtension))
            throw new DomainException(DomainErrors.ImageErrors.FileExtensionRequired);
        if (fileExtension.Length > 8)
            throw new DomainException(DomainErrors.ImageErrors.FileExtensionTooLong);

        if (imageTypeId <= 0)
            throw new DomainException(DomainErrors.ImageErrors.InvalidImageTypeId);

        //link & linkTarget.
        if (!string.IsNullOrWhiteSpace(link))
        {
            //link.
            if (!Uri.TryCreate(link, UriKind.RelativeOrAbsolute, out _))
                throw new DomainException(DomainErrors.ImageErrors.InvalidLink);

            //linkTarget.
            if (string.IsNullOrWhiteSpace(linkTarget))
                throw new DomainException(DomainErrors.ImageErrors.LinkTargetRequiredWhenLinkProvided);
            if (linkTarget != "_self" && linkTarget != "_blank")
                throw new DomainException(DomainErrors.ImageErrors.InvalidLinkTarget);
        }

        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.ImageErrors.InvalidCreatedBy);

        OgFileName = ogFileName;
        FileName = fileName;
        FileExtension = fileExtension;
        ImageTypeId = imageTypeId;
        Size = size;
        AltText = altText;
        Title = title;
        Loading = loading;
        Link = link;
        LinkTarget = linkTarget;
        SortOrder = sortOrder;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
        CreatedByIp = createdByIp;
    }

    public static Image Create(Type imageType, string ogFileName, string fileName, string fileExtension,
        int imageTypeId, ImageSize size, string? altText, string? title, ImageLoading loading, string? link,
        string? linkTarget, int parentId, int sortOrder, int createdBy, DateTime createdAt, IPAddress createdByIp)
    {
        if (imageType == typeof(BrandImage))
            return new BrandImage(ogFileName, fileName, fileExtension, imageTypeId, size, altText, title, loading, link,
                linkTarget, parentId, sortOrder, createdBy, createdAt, createdByIp);

        if (imageType == typeof(CategoryImage))
            return new CategoryImage(ogFileName, fileName, fileExtension, imageTypeId, size, altText, title, loading,
                link, linkTarget, parentId, sortOrder, createdBy, createdAt, createdByIp);

        if (imageType == typeof(ProductImage))
            return new ProductImage(ogFileName, fileName, fileExtension, imageTypeId, size, altText, title, loading,
                link, linkTarget, parentId, sortOrder, createdBy, createdAt, createdByIp);

        if (imageType == typeof(ProductGroupImage))
            return new ProductGroupImage(ogFileName, fileName, fileExtension, imageTypeId, size, altText, title,
                loading, link, linkTarget, parentId, sortOrder, createdBy, createdAt, createdByIp);

        if (imageType == typeof(BankImage))
            return new BankImage(ogFileName, fileName, fileExtension, imageTypeId, size, altText, title, loading,
                link, linkTarget, parentId, sortOrder, createdBy, createdAt, createdByIp);

        if (imageType == typeof(PageImage))
            return new PageImage(ogFileName, fileName, fileExtension, imageTypeId, size, altText, title, loading,
                link, linkTarget, parentId, sortOrder, createdBy, createdAt, createdByIp);

        ICreatable.ValidateCreatedBy(createdBy, DomainErrors.ImageErrors.InvalidCreatedBy);

        throw new DomainException(DomainErrors.ImageErrors.InvalidImageType);
    }

    public void Update(int imageTypeId, ImageSize size, string? altText, string? title, ImageLoading loading,
        string? link, string? linkTarget, int sortOrder, int updatedBy, IPAddress updatedByIp)
    {
        IUpdatable.ValidateUpdatedBy(updatedBy, DomainErrors.ImageErrors.InvalidUpdatedBy);

        if (ImageTypeId == imageTypeId && Size == size && AltText == altText && Title == title && Loading == loading &&
            Link == link && LinkTarget == linkTarget && SortOrder == sortOrder)
            return;

        if (imageTypeId <= 0)
            throw new DomainException(DomainErrors.ImageErrors.InvalidImageTypeId);

        //link & linkTarget.
        if (!string.IsNullOrWhiteSpace(link))
        {
            //link.
            if (!Uri.TryCreate(link, UriKind.RelativeOrAbsolute, out _))
                throw new DomainException(DomainErrors.ImageErrors.InvalidLink);

            //linkTarget.
            if (string.IsNullOrWhiteSpace(linkTarget))
                throw new DomainException(DomainErrors.ImageErrors.LinkTargetRequiredWhenLinkProvided);
            if (linkTarget != "_self" && linkTarget != "_blank")
                throw new DomainException(DomainErrors.ImageErrors.InvalidLinkTarget);
        }

        ImageTypeId = imageTypeId;
        Size = size;
        AltText = altText;
        Title = title;
        Loading = loading;
        Link = link;
        LinkTarget = linkTarget;
        SortOrder = sortOrder;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;
    }

    public void Delete(int deletedBy, DateTime deletedAt, IPAddress deletedByIp)
    {
        IDeletable.ValidateDeletedBy(deletedBy, DomainErrors.ImageErrors.InvalidDeletedBy);

        DeletedBy = deletedBy;
        DeletedAt = deletedAt;
        DeletedByIp = deletedByIp;
    }
}