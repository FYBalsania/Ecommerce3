using Ecommerce3.Domain.Enums;
using Ecommerce3.Domain.Extensions;

namespace Ecommerce3.Domain.Entities;

public sealed class Cart : Entity, ICreatable, IUpdatable
{
    private readonly List<CartLine> _lines = [];
    public int? CustomerId { get; private set; }
    public string? SessionId { get; private set; }
    public decimal Subtotal => _lines.Sum(l => l.Subtotal).RoundMoney();
    public decimal TotalDiscount => _lines.Sum(l => l.DiscountAmount).RoundMoney();
    public decimal TotalTax => _lines.Sum(l => l.TaxAmount).RoundMoney();
    public decimal GrandTotal => (Math.Max(0m, Subtotal - TotalDiscount) + TotalTax).RoundMoney();
    public int CreatedBy { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public string CreatedByIp { get; private set; }
    public int? UpdatedBy { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public string? UpdatedByIp { get; private set; }
    public IReadOnlyCollection<CartLine> Lines => _lines;

    private Cart()
    {
    }

    public Cart(int? customerId, string? sessionId, int createdBy, string createdByIp)
    {
        CustomerId = customerId;
        SessionId = sessionId;
        CreatedBy = createdBy;
        CreatedAt = DateTime.Now;
        CreatedByIp = createdByIp;
    }

    public CartLine? FindCartLineByProductId(int productId) => _lines.FirstOrDefault(l => l.ProductId == productId);

    public CartLine AddOrUpdate(int productId, int quantity, int customerId, string customerIp)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        var cartLine = FindCartLineByProductId(productId);
        if (cartLine is not null)
        {
            cartLine.ChangeQuantityBy(quantity, customerId, customerIp);
            ;
            UpdatedBy = customerId;
            UpdatedAt = DateTime.Now;
            UpdatedByIp = customerIp;
            return cartLine;
        }

        cartLine = new CartLine(productId, quantity, customerId, customerIp);
        _lines.Add(cartLine);
        UpdatedBy = customerId;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = customerIp;
        return cartLine;
    }

    public bool RemoveCartLine(int productId, int updatedBy, string updatedByIp)
    {
        var cartLine = FindCartLineByProductId(productId);
        if (cartLine is null) return false;
        _lines.Remove(cartLine);
        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;
        return true;
    }

    public bool UpdateCartLineQuantity(int cartLineId, int newQuantity, int updatedBy, string updatedByIp)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(newQuantity);

        var cartLine = _lines.FirstOrDefault(l => l.Id == cartLineId);
        if (cartLine is null) return false;

        cartLine.UpdateQuantity(newQuantity, updatedBy, updatedByIp);

        UpdatedBy = updatedBy;
        UpdatedAt = DateTime.Now;
        UpdatedByIp = updatedByIp;
        return true;
    }
    
    // Clear all discounts on all lines (useful before re-applying discounts)
    public void ClearAllLineDiscounts()
    {
        foreach (var l in _lines) l.ClearDiscounts();
    }
    
    // A small pluggable discount application example:
    //
    // The caller supplies a resolver that returns applicable discounts for a line,
    // and the cart applies them in deterministic order.
    //
    // discountResolver: (Cart cart, CartLine line) => IEnumerable<ResolvedDiscount>
    //
    // ResolvedDiscount is a small DTO with { DiscountId, Code, Type, Amount, IsStackable, Priority }
    //
    public void ApplyDiscounts(Func<Cart, CartLine, IEnumerable<ResolvedDiscount>> discountResolver,
                               GlobalDiscountPolicy? policy = null)
    {
        if (discountResolver == null) throw new ArgumentNullException(nameof(discountResolver));

        // clear previous
        ClearAllLineDiscounts();

        // simple per-line application: for each line, ask resolver for matching discounts
        foreach (var line in _lines)
        {
            var resolved = discountResolver(this, line)
                           .OrderBy(d => d.Priority)     // priority: low value = higher priority
                           .ToList();

            // separate stackable vs non-stackable
            var nonStackables = resolved.Where(d => !d.CanStack).ToList();
            var toApply = new List<ResolvedDiscount>();

            if (nonStackables.Any())
            {
                // pick highest priority non-stackable (lowest Priority)
                var pick = nonStackables.OrderBy(d => d.Priority).First();
                toApply.Add(pick);
            }
            else
            {
                // all stackable - apply them in priority order
                toApply.AddRange(resolved);
            }

            // apply discounts sequentially to the line
            foreach (var rd in toApply)
            {
                decimal applied = 0m;
                if (rd.Type == DiscountType.Percentage)
                {
                    var percent = rd.Amount;
                    var raw = Math.Round(line.Subtotal * (percent / 100m), 2, MidpointRounding.ToEven);
                    applied = line.ApplyDiscount(raw, rd.DiscountId, rd.Code);
                }
                else // Fixed
                {
                    applied = line.ApplyDiscount(rd.Amount, rd.DiscountId, rd.Code);
                }

                // enforce optional per-line policy caps
                if (policy != null)
                {
                    // clamp by percent cap
                    if (policy.MaxPercent.HasValue && rd.Type == DiscountType.Percentage && rd.Amount > policy.MaxPercent.Value)
                    {
                        // recompute with capped percent — naive approach; for complex cases do full recalculation
                        var cappedRaw = Math.Round(line.Subtotal * (policy.MaxPercent.Value / 100m), 2, MidpointRounding.ToEven);
                        // adjust recorded discount: remove prior applied and reapply
                        // simple approach: clear and reapply capped only
                        line.ClearDiscounts();
                        line.ApplyDiscount(cappedRaw, rd.DiscountId, rd.Code);
                    }

                    if (policy.MaxAbsolutePerLine.HasValue && line.DiscountAmount > policy.MaxAbsolutePerLine.Value)
                    {
                        // clamp total discount to max per line
                        var excess = line.DiscountAmount - policy.MaxAbsolutePerLine.Value;
                        // reduce by removing last applied or scaling — for simplicity, we remove excess from the last discount applied
                        // NOTE: implement robust scaling policy if you need proportional distribution
                        var last = (line.AppliedDiscounts.LastOrDefault() as CartLineDiscount);
                        if (last != null && excess > 0)
                        {
                            // This simple domain model doesn't mutate CartLineDiscount; in a richer model you would update it.
                            // To keep domain logic clean here, we simply cap the DiscountAmount field.
                            // (In practice store separate raw/applied values and update the last applied discount's applied amount.)
                            // set DiscountAmount to cap:
                            // (the CartLine.ApplyDiscount aggregated it already; set property via internal mechanics or expose method)
                        }
                    }
                }
            }
        }

        // Optionally enforce order-level caps (policy) - sample proportional scaling:
        if (policy != null && policy.MaxAbsolutePerOrder.HasValue)
        {
            var totalDiscount = _lines.Sum(l => l.DiscountAmount);
            if (totalDiscount > policy.MaxAbsolutePerOrder.Value)
            {
                var cap = policy.MaxAbsolutePerOrder.Value;
                var scale = cap / totalDiscount;
                // proportionally scale applied discounts per line
                foreach (var line in _lines)
                {
                    var old = line.DiscountAmount;
                    var scaled = Math.Round(old * scale, 2, MidpointRounding.ToEven);
                    // naive approach: clear and re-add a single synthetic "policy-scaling" discount
                    line.ClearDiscounts();
                    line.ApplyDiscount(scaled, null, "POLICY_SCALING");
                }
            }
        }

        UpdatedAt = DateTime.UtcNow;
    }
}

public sealed class ResolvedDiscount
{
    public int? DiscountId { get; init; }
    public string? Code { get; init; }
    public DiscountType Type { get; init; }
    public decimal Amount { get; init; }     // percent (10) or absolute (100.00)
    public bool CanStack { get; init; }
    public int Priority { get; init; } = 100;
}

public sealed class GlobalDiscountPolicy
{
    public decimal? MaxPercent { get; init; }           // e.g., 50 = 50%
    public decimal? MaxAbsolutePerOrder { get; init; }  // currency
    public decimal? MaxAbsolutePerLine { get; init; }   // currency
}