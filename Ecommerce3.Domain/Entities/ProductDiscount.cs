namespace Ecommerce3.Domain.Entities;

public sealed class ProductDiscount : Discount
{
    private readonly List<DiscountProduct> _products = [];
    public IReadOnlyCollection<DiscountProduct> Products => _products;
}