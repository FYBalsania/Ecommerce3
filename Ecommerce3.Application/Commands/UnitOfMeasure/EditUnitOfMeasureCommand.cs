using System.Net;
using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Application.Commands.UnitOfMeasure;

public record EditUnitOfMeasureCommand
{
    public int Id { get; init; }
    public required string Code { get; init; }
    public required string SingularName { get; init; }
    public required string PluralName { get; init; }
    public required UnitOfMeasureType Type { get; init; }
    public int? BaseId { get; init; }
    public required decimal ConversionFactor { get; init; }
    public required byte DecimalPlaces { get; init; }
    public required bool IsActive { get; init; }
    public required int UpdatedBy { get; init; }
    public required DateTime UpdatedAt { get; init; }
    public required IPAddress UpdatedByIp { get; init; }
}