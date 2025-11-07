using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Domain.Policies;

/// <summary>
/// Domain-level abstraction for detecting/verifying image bytes.
/// Implementation belongs to Infrastructure.
/// </summary>
public interface IImageTypeDetector
{
    ImageKind Detect(byte[] fileBytes);

    /// <summary>
    /// Throw a domain-specific exception or return false depending on your style.
    /// </summary>
    void EnsureMatchesExtension(byte[] fileBytes, string fileName);
}