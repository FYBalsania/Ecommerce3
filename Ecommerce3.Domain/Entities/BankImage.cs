using System.Net;
using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Errors;
using Ecommerce3.Domain.Exceptions;

namespace Ecommerce3.Domain.Entities;

public sealed class BankImage : Image
{
    public int BankId { get; private set; }
    public Bank? Bank { get; private set; }

    private BankImage() : base()
    {
    }

    internal BankImage(string ogFileName, string fileName, string fileExtension, int imageTypeId, ImageSize size,
        string? altText, string? title, ImageLoading loading, string? link, string? linkTarget, int bankId,
        int sortOrder, int createdBy, DateTime createdAt, IPAddress createdByIp)
        : base(ogFileName, fileName, fileExtension, imageTypeId, size, altText,
            title, loading, link, linkTarget, sortOrder, createdBy, createdAt, createdByIp)
    {
        if (bankId <= 0) throw new DomainException(DomainErrors.ImageErrors.InvalidBankId);
        BankId = bankId;
    }
}