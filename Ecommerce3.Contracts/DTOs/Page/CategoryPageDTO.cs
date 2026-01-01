namespace Ecommerce3.Contracts.DTOs.Page;

public record CategoryPageDTO :  PageDTO
{
    public int? CategoryId { get; init; }
    public string? CategoryName { get; set; }
}