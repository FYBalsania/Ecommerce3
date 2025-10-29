namespace Ecommerce3.Application.Commands.Bank;

public record AddBankCommand
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Slug { get; init; }
    public bool IsActive { get; init; }
    public int SortOrder { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public string CreatedByIp { get; init; }
}