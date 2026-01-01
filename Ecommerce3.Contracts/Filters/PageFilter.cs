namespace Ecommerce3.Contracts.Filters;

public sealed record PageFilter
{
    public string? Type { get; set; }
    public string? Path { get; init; }
    public string? MetaTitle { get; init; }
    public bool? IsActive { get; init; }
}
