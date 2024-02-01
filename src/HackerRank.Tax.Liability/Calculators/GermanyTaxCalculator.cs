class GermanyTaxCalculator : BaseTaxCalculator
{
    protected override async Task<decimal> Calculate(decimal total) => total switch
    {
        <= 9744 => 0,
        <= 57919 => (total - 9744) * 0.4m,
        _ => (total - 57919) * 0.4m + total * 0.05m
    };
}
