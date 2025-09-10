namespace Ecommerce3.Domain.Entities;

public sealed class ProductCategory
{
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public bool IsPrimary { get; set; }
    public int SortOrder { get; set; }
}