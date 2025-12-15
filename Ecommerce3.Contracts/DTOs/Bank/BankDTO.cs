using Ecommerce3.Contracts.DTOs.Image;

namespace Ecommerce3.Contracts.DTOs.Bank;

public class BankDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
    public string MetaTitle { get; set; }
    public string? MetaDescription { get; set; }
    public string? MetaKeywords { get; set; }
    public string? H1 { get; set; }
    public IReadOnlyList<ImageDTO> Images { get; set; } = [];
}