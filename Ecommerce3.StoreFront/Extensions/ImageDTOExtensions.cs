using Ecommerce3.Contracts.DTO.StoreFront.Image;

namespace Ecommerce3.StoreFront.Extensions;

public static class ImageDTOExtensions
{
    extension(IReadOnlyList<ImageDTO> images)
    {
        public IEnumerable<ImageDTO> FilterByImageTypeId(int imageTypeId)
            => images.Where(x => x.ImageTypeId == imageTypeId);
        
    }
}