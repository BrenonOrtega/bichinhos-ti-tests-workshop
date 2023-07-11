class ItalyTaxCalculator : BaseTaxCalculator
{
    protected override decimal Calculate(decimal total) => total switch
    {
        <= 15000 => total * 0.23m,
        <= 28000 => (total - 15000) * 0.27m + 15000 * 0.23m,
        <= 55000 => (total - 28000) * 0.38m + 13000 * 0.27m + 15000 * 0.23m,
        _ => (total - 55000) * 0.41m + 27000 * 0.38m + 13000 * 0.27m + 15000 * 0.23m
    };
}
