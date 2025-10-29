namespace Ecommerce3.Contracts.DTOs.Bank;

public class BankDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
}