namespace Ecommerce3.Domain.Entities;

public class ProductProductAttribute : Entity
{
    public int ProductId { get; private set; }
    public int ProductAttributeId { get; private set; }
    public int ProductAttributeSortOrder { get; private set; }
    public int ProductAttributeValueId { get; private set; }
    public int ProductAttributeValueSortOrder { get; private set; }
}