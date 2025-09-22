namespace Ecommerce3.Application.DTOs;

public class BrandListItemDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string CreatedUserFullName { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? CreatedByIp { get; set; }
}