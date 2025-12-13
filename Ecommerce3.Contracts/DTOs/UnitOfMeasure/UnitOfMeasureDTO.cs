using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.DTOs.UnitOfMeasure;

public record UnitOfMeasureDTO
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public UnitOfMeasureType Type { get; set; }
    public int? BaseId { get; set; }
    public Domain.Entities.UnitOfMeasure? Base { get; set; }
    public decimal ConversionFactor { get; set; }
    public bool IsActive { get; set; }
}