namespace Ecommerce3.Domain.Extensions;

public static class DecimalExtensions
{
    public static decimal RoundMoney(this decimal value)
        => Math.Round(value, 2, MidpointRounding.ToEven);
}