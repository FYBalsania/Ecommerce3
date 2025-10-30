using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Entities;

public sealed class BankImage : Image
{
    public int BankId { get; private set; }
    public Bank? Bank { get; private set; }
    
    public BankImage(string ogFileName, string fileName, string fileExtension, int imageTypeId, ImageSize size,
        string? altText, string? title, string loading, string? link, string? linkTarget, int bankId, int sortOrder, int createdBy,
        DateTime createdAt, string createdByIp)
        : base(ogFileName, fileName, fileExtension, imageTypeId, size, altText,
            title, loading, link, linkTarget, sortOrder, createdBy, createdAt, createdByIp)
    {
        BankId = bankId;
    }
}