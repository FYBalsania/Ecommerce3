using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Commands.DeliveryWindow;

public record AddDeliveryWindowCommand
{
    public string Name { get; init; }
    public DeliveryUnit Unit { get; init; }
    public int MinValue { get; init; }
    public int? MaxValue { get; init; }
    public bool IsActive { get; init; }
    public int SortOrder { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedAt { get; init; }
    public string CreatedByIp { get; init; }
}