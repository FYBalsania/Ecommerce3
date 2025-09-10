namespace Ecommerce3.Domain.Entities;

public sealed class CartLineDiscount : Entity
{
    public int CartLineId { get; private set; }
    public int? DiscountId { get; private set; }
    public decimal DiscountAmount { get; private set; }
    
    private CartLineDiscount() { }

    public CartLineDiscount(int? discountId, decimal discountAmount)
    {
        DiscountId = discountId;
        DiscountAmount = discountAmount;
    }
}