namespace Ecommerce3.Application.Commands.Bank;

public record EditBankCommand
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Slug { get; init; }
    public bool IsActive { get; init; }
    public int SortOrder { get; init; }
    public int UpdatedBy { get; init; }
    public DateTime UpdatedAt { get; init; }
    public string UpdatedByIp { get; init; }
}