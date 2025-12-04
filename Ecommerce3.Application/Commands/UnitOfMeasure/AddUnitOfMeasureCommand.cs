using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Commands.UnitOfMeasure;

public record AddUnitOfMeasureCommand
{
    public required string Code { get; init; }
    public required string Name { get; init; }
    public required UnitOfMeasureType Type { get; init; }
    public int? BaseId { get; init; }
    public required decimal ConversionFactor { get; init; }
    public required bool IsActive { get; init; }
    public required int CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required string CreatedByIp { get; init; }
}