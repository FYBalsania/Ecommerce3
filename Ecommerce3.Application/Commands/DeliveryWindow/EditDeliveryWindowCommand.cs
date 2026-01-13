using System.Net;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Commands.DeliveryWindow;

public record EditDeliveryWindowCommand
{
    public int Id { get; init; }
    public string Name { get; init; }
    public DeliveryUnit Unit { get; init; }
    public int MinValue { get; init; }
    public int? MaxValue { get; init; }
    public bool IsActive { get; init; }
    public int SortOrder { get; init; }
    public int UpdatedBy { get; init; }
    public DateTime UpdatedAt { get; init; }
    public IPAddress UpdatedByIp { get; init; }

}