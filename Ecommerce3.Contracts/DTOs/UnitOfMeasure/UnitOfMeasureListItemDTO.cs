using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.DTOs.UnitOfMeasure;

public record UnitOfMeasureListItemDTO
{
    public int Id { get; init; }
    public string Code { get; init; }
    public string Name { get; init; }
    public UnitOfMeasureType Type { get; init; }
    public string? BaseName { get; init; }
    public decimal ConversionFactor { get; init; }    
    public bool IsActive { get; init; }
    public string CreatedUserFullName { get; init; }
    public DateTime CreatedAt { get; init; }
}