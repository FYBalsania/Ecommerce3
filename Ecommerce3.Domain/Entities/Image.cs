using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public class Image : Entity, ICreatable, IUpdatable, IDeletable
{
    public string Discriminator { get; private set; }
    public string OgFileName { get; private set; }
    public string FileName { get; private set; }
    public string FileExtension { get; private set; }
    public int ImageTypeId { get; private set; }
    public ImageType? ImageType { get; private set; }
    public ImageSize Size { get; private set; }
    public string? AltText { get; private set; }
    public string? Title { get; private set; }
    public string Loading { get; private set; }
    public string? Link { get; private set; }
    public string? LinkTarget { get; private set; }
    public int SortOrder { get; private set; }
    public int CreatedBy { get; private set; }
    public IAppUser? CreatedByUser { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public IAppUser? UpdatedByUser { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public IAppUser? DeletedByUser { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }
    
    private protected Image()
    {
    }

    internal Image(string ogFileName, string fileName, string fileExtension, int imageTypeId, ImageSize size,
        string? altText, string? title, string loading, string? link, string? linkTarget, int sortOrder, int createdBy,
        DateTime createdAt, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ogFileName, nameof(ogFileName));
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName, nameof(fileName));
        ArgumentException.ThrowIfNullOrWhiteSpace(fileExtension, nameof(fileExtension));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(imageTypeId, 0, nameof(imageTypeId));
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(createdBy, 0, nameof(createdBy));
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));

        //loading.
        ArgumentException.ThrowIfNullOrWhiteSpace(loading, nameof(loading));
        if (loading != "eager" && loading != "lazy")
        {
            throw new ArgumentException("Loading must be either 'eager' or 'lazy'", nameof(loading));
        }

        //link & linkTarget.
        if (!string.IsNullOrWhiteSpace(link))
        {
            //link.
            if (!Uri.TryCreate(link, UriKind.RelativeOrAbsolute, out _))
                throw new ArgumentException("Link must be a valid URL", nameof(link));

            //linkTarget.
            if (string.IsNullOrWhiteSpace(linkTarget))
                throw new ArgumentException("LinkTarget is required when Link is provided",
                    nameof(linkTarget));
            if (linkTarget != "_self" && linkTarget != "_blank")
                throw new ArgumentException("LinkTarget must be either '_self' or '_blank'",
                    nameof(linkTarget));
        }

        //linkTarget
        if (link != null)
        {
            if (string.IsNullOrWhiteSpace(linkTarget))
            {
                throw new ArgumentException("LinkTarget is required when Link is provided", nameof(linkTarget));
            }

            if (linkTarget != "_self" && linkTarget != "_blank")
            {
                throw new ArgumentException("LinkTarget must be either '_self' or '_blank'", nameof(linkTarget));
            }
        }

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
        int imageTypeId, ImageSize size, string? altText, string? title, string loading, string? link,
        string? linkTarget, int parentId, int sortOrder, int createdBy, DateTime createdAt, string createdByIp)
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

        throw new ArgumentException("Invalid image type", nameof(imageType));
    }
}