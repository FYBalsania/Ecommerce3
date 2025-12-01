using Ecommerce3.Domain.Entities;

namespace Ecommerce3.Domain.Errors;

public static partial class DomainErrors
{
    public static class ProductAttributeColourValueErrors
    {
        public static readonly DomainError HexCodeTooLong =
            new($"{nameof(ProductAttributeColourValue)}.{nameof(ProductAttributeColourValue.HexCode)}",
                "Hex code cannot exceed 8 characters.");

        public static readonly DomainError ColourFamilyRequired =
            new($"{nameof(ProductAttributeColourValue)}.{nameof(ProductAttributeColourValue.ColourFamily)}",
                "Colour family is required.");

        public static readonly DomainError ColourFamilyTooLong =
            new($"{nameof(ProductAttributeColourValue)}.{nameof(ProductAttributeColourValue.ColourFamily)}",
                "Colour family cannot exceed 64 characters.");

        public static readonly DomainError ColourFamilyHexCodeTooLong =
            new($"{nameof(ProductAttributeColourValue)}.{nameof(ProductAttributeColourValue.ColourFamilyHexCode)}",
                "Colour family hex code cannot exceed 8 characters.");
    }
}