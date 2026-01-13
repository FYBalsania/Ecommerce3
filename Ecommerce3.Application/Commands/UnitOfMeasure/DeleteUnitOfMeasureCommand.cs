using System.Net;

namespace Ecommerce3.Application.Commands.UnitOfMeasure;

public record DeleteUnitOfMeasureCommand
{
    public int Id { get; init; }
    public required int DeletedBy { get; init; }
    public required DateTime DeletedAt { get; init; }
    public required IPAddress DeletedByIp { get; init; }
}