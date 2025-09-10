using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class Image : Entity, ICreatable, IUpdatable, IDeletable
{
    public string OgFileName { get; private set; }
    public string FileName { get; private set; }
    public string FileExtension { get; private set; }
    public ImageType Type { get; private set; }
    public ImageSize Size { get; private set; }
    public string? AltText { get; private set; }
    public string? Title { get; private set; }
    public string Loading { get; private set; }
    public string? Link { get; private set; }
    public string? LinkTarget { get; private set; }
    public int SortOrder { get; private set; }
    public int? BrandId { get; private set; }
    public int? ProductId { get; private set; }
    public int? CategoryId { get; private set; }
    public int? PageId { get; private set; }
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }

    public Image(string ogFileName, string fileName, string fileExtension, ImageType type, ImageSize size,
        string? altText, string? title, string loading, string? link, string? linkTarget, int sortOrder, int? brandId,
        int? productId, int? categoryId, int? pageId, int createdBy, string createdByIp)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(ogFileName, nameof(ogFileName));
        ArgumentException.ThrowIfNullOrWhiteSpace(fileName, nameof(fileName));
        ArgumentException.ThrowIfNullOrWhiteSpace(fileExtension, nameof(fileExtension));
        ArgumentException.ThrowIfNullOrWhiteSpace(loading, nameof(loading));
        if (loading != "eager" && loading != "lazy")
        {
            throw new ArgumentException("Loading must be either 'eager' or 'lazy'", nameof(loading));
        }
        //link
        if (link != null && !Uri.TryCreate(link, UriKind.Absolute, out _))
        {
            throw new ArgumentException("Link must be a valid URL", nameof(link));
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
        //brandId, productId, categoryId, pageId
        if (brandId is null && productId is null && categoryId is null && pageId is null)
        {
            throw new ArgumentException("At least one of BrandId, ProductId, CategoryId, or PageId must be provided",
                nameof(brandId));
        }
        //createdByIp
        ArgumentException.ThrowIfNullOrWhiteSpace(createdByIp, nameof(createdByIp));
        
        
        OgFileName = ogFileName;
        FileName = fileName;
        FileExtension = fileExtension;
        Type = type;
        Size = size;
        AltText = altText;
        Title = title;
        Loading = loading;
        Link = link;
        LinkTarget = linkTarget;
        SortOrder = sortOrder;
        BrandId = brandId;
        ProductId = productId;
        CategoryId = categoryId;
        PageId = pageId;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;       
        CreatedByIp = createdByIp;
    }
}