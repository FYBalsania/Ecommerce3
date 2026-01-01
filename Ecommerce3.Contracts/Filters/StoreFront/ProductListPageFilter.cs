namespace Ecommerce3.Contracts.Filters.StoreFront;

public record ProductListPageFilter
{
    public string[]? Brands { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public Domain.Enums.SortOrder SortOrder { get; set; }
    public IDictionary<string, decimal>? Weights { get; set; }
    public IDictionary<string, string>? Attributes { get; set; }
}