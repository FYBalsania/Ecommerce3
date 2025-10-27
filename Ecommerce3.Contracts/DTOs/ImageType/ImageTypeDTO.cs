namespace Ecommerce3.Contracts.DTOs.ImageType;

public class ImageTypeDTO
{
    public int Id { get; set; }
    public string? Entity { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}