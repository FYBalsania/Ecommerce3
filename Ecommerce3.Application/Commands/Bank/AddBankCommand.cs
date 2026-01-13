using System.Net;

namespace Ecommerce3.Application.Commands.Bank;

public record AddBankCommand
{
    public required string Name { get; init; }
    public required string Slug { get; init; }
    public required bool IsActive { get; init; }
    public required int SortOrder { get; init; }
    public required string MetaTitle { get; init; }
    public string? MetaDescription { get; init; }
    public string? MetaKeywords { get; init; }
    public required string H1 { get; init; }
    public required int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public IPAddress CreatedByIp { get; init; }
}