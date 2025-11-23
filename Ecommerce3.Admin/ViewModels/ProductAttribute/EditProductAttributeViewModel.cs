using Ecommerce3.Domain.Enums;

namespace Ecommerce3.Admin.ViewModels.ProductAttribute;

public sealed class EditProductAttributeViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public DataType DataType { get; set; }
}