using Ecommerce3.Contracts.DTO.StoreFront.Image;

namespace Ecommerce3.StoreFront.Extensions;

public static class ImageDTOExtensions
{
    extension(IReadOnlyList<ImageDTO> images)
    {
        public IEnumerable<ImageDTO> FilterByImageTypeId(int imageTypeId)
            => images.Where(x => x.ImageTypeId == imageTypeId);

        public IEnumerable<ImageDTO> FilterByImageTypeSlug(string imageTypeSlug)
            => images.Where(x => x.ImageTypeSlug.Equals(imageTypeSlug, StringComparison.OrdinalIgnoreCase));

        public IEnumerable<ImageDTO> FilterByImageTypeName(string imageTypeName)
            => images.Where(x => x.ImageTypeName.Equals(imageTypeName, StringComparison.OrdinalIgnoreCase));
    }
}