using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.Filters;

public sealed record DeliveryWindowFilter
{
    public string? Name { get; init; }
    public DeliveryUnit? Unit { get; init; }
    public int? MinValue { get; init; }
    public int? MaxValue { get; init; }
    public bool? IsActive { get; init; }
}