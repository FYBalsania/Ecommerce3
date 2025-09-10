using Ecommerce3.Domain.Extensions;

namespace Ecommerce3.Domain.Entities;

public sealed class CartLine : Entity, ICreatable, IUpdatable, IDeletable
{
    private readonly List<CartLineDiscount> _appliedDiscounts = [];
    
    public int CartId { get; private set; }
    public int ProductId { get; private set; }
    public Product Product { get; private set; }
    public int Quantity { get; private set; }
    public decimal Subtotal => (Product.Price * Quantity).RoundMoney();
    public decimal DiscountAmount { get; private set; }
    public decimal TaxAmount { get; private set; } = 0m;
    public decimal FinalLineTotal => (Math.Max(0m, Subtotal - DiscountAmount) + TaxAmount).RoundMoney();
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public int? DeletedBy { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public string? DeletedByIp { get; private set; }
    
    public IReadOnlyCollection<CartLineDiscount> AppliedDiscounts => _appliedDiscounts.AsReadOnly();
    
    private CartLine()
    {
    }

    public CartLine(int productId, int quantity, int createdBy, string createdByIp)
    {
        ProductId = productId;
        Quantity = quantity;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }
    
    // Update quantity (returns final quantity)
    public int UpdateQuantity(int newQuantity, int updatedBy, string updatedByIp)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(newQuantity,0, nameof(newQuantity));
        
        Quantity = newQuantity;
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;
        // optionally reset discount/tax snapshots if your policy requires recalculation
        return Quantity;
    }
    
    // Increase or decrease
    public int ChangeQuantityBy(int delta, int updatedBy, string updatedByIp)
    {
        return UpdateQuantity(Math.Max(0, Quantity + delta), updatedBy, updatedByIp);
    }
    
    // Clear discounts (useful before re-applying)
    public void ClearDiscounts()
    {
        _appliedDiscounts.Clear();
        DiscountAmount = 0m;
    }
    
    // Apply an absolute discount amount to this line (records snapshot)
    // returns applied amount after clamping (non-negative and <= subtotal)
    public decimal ApplyDiscount(decimal amount, int? discountId = null, string? discountCode = null)
    {
        if (amount <= 0) return 0m;

        // clamp to remaining subtotal
        var maxAllowed = Subtotal - DiscountAmount;
        var toApply = Math.Min(amount, maxAllowed);
        toApply = Math.Max(0m, toApply).RoundMoney();

        if (toApply <= 0m) return 0m;

        DiscountAmount = (DiscountAmount + toApply).RoundMoney();
        _appliedDiscounts.Add(new CartLineDiscount(discountId, toApply));
        return toApply;
    }

    // Apply a percentage discount (e.g., 10 => 10%)
    public decimal ApplyPercentageDiscount(decimal percent, int? discountId = null, string? discountCode = null)
    {
        if (percent <= 0m) return 0m;
        // percent is expressed as whole number: 10 = 10%
        var raw = Subtotal * (percent / 100m);
        var applied = ApplyDiscount(raw, discountId, discountCode);
        return applied;
    }
}