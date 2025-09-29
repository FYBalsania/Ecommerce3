namespace Ecommerce3.Domain.Entities;

public sealed class BrandCategoryPage : Page
{
    // public int BrandId { get; set; }
    public Brand? Brand { get; private set; }
    // public int CategoryId { get; private set; }
    public Category? Category { get; private set; } 
}