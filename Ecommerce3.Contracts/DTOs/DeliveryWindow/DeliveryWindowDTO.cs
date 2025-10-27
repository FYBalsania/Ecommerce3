using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Contracts.DTOs.DeliveryWindow;

public class DeliveryWindowDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DeliveryUnit Unit { get; set; }
    public int MinValue { get; set; }
    public int? MaxValue { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
}