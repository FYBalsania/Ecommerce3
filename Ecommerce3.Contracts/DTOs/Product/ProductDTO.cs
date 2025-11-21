using Ecommerce3.Contracts.DTOs.Image;

namespace Ecommerce3.Contracts.DTOs.Product;

public record ProductDTO
{
    public int Id { get; set; }
    public IReadOnlyList<ImageDTO> Images { get; set; } = [];
}