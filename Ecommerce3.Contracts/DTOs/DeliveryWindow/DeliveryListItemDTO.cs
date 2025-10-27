using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.DTOs.DeliveryWindow;

public record DeliveryListItemDTO
{
    public int Id { get; init; }
    public string Name { get; init; }
    public DeliveryUnit Unit { get; init; }
    public int MinValue { get; init; }
    public int? MaxValue { get; init; }
    public decimal NormalizedMinDays { get; init; }
    public decimal? NormalizedMaxDays { get; init; }
    public int SortOrder { get; init; }
    public bool IsActive { get; init; }
    public string CreatedUserFullName { get; init; }
    public DateTime CreatedAt { get; init; }
}