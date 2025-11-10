using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Policies;

namespace Ecommerce3.Infrastructure.Imaging;

internal class FileSignatureImageTypeDetector : IImageTypeDetector
{
    public ImageKind Detect(byte[] fileBytes)
    {
        if (fileBytes == null || fileBytes.Length < 12)
            return ImageKind.Unknown;

        // JPEG
        if (fileBytes.Take(3).SequenceEqual(new byte[] { 0xFF, 0xD8, 0xFF }))
            return ImageKind.Jpeg;

        // PNG
        if (fileBytes.Length >= 8 &&
            fileBytes.Take(8).SequenceEqual(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }))
            return ImageKind.Png;

        // GIF
        if (fileBytes.Take(4).SequenceEqual(new byte[] { 0x47, 0x49, 0x46, 0x38 })) // "GIF8"
            return ImageKind.Gif;

        // BMP
        if (fileBytes.Take(2).SequenceEqual(new byte[] { 0x42, 0x4D })) // "BM"
            return ImageKind.Bmp;

        // WEBP (RIFF....WEBP)
        if (fileBytes.Length >= 12 &&
            fileBytes[0] == 0x52 && fileBytes[1] == 0x49 && fileBytes[2] == 0x46 && fileBytes[3] == 0x46 && // "RIFF"
            fileBytes[8] == 0x57 && fileBytes[9] == 0x45 && fileBytes[10] == 0x42 && fileBytes[11] == 0x50)   // "WEBP"
            return ImageKind.WebP;

        return ImageKind.Unknown;
    }

    public void EnsureMatchesExtension(byte[] fileBytes, string fileName)
    {
        var ext = Path.GetExtension(fileName)?.ToLowerInvariant();
        var kind = Detect(fileBytes);

        var ok = ext switch
        {
            ".jpg" or ".jpeg" => kind == ImageKind.Jpeg,
            ".png" => kind == ImageKind.Png,
            ".gif" => kind == ImageKind.Gif,
            ".bmp" => kind == ImageKind.Bmp,
            ".webp" => kind == ImageKind.WebP,
            _ => false
        };

        if (!ok)
            throw new InvalidOperationException($"File content does not match its extension ({ext ?? "unknown"}).");
    }
}