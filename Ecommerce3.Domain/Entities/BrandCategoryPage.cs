namespace Ecommerce3.Domain.Entities;

public sealed class BrandCategoryPage : Page
{
    public Brand? Brand { get; private set; }
    public Category? Category { get; private set; } 
}