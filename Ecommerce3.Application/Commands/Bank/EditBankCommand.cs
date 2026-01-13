using System.Net;

namespace Ecommerce3.Application.Commands.Bank;

public record EditBankCommand
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public required bool IsActive { get; init; }
    public required int SortOrder { get; init; }
    public required string MetaTitle { get; init; }
    public string? MetaDescription { get; init; }
    public string? MetaKeywords { get; init; }
    public required string H1 { get; init; }
    public required int UpdatedBy { get; init; }
    public DateTime UpdatedAt { get; init; }
    public required IPAddress UpdatedByIp { get; init; }
}