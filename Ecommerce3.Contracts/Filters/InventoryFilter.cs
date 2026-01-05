using System.ComponentModel.DataAnnotations;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.Filters;

public sealed record InventoryFilter
{
    public string? Name { get; init; }
    public string? SKU { get; init; }
    public decimal? MinPrice { get; init; }
    public decimal? MaxPrice { get; init; }
    public decimal? MinStock { get; init; }
    public decimal? MaxStock { get; init; }
    public StockStatus? StockStatus { get; init; }
}