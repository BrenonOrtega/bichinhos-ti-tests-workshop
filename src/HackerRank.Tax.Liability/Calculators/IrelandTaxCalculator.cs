class IrelandTaxCalculator : BaseTaxCalculator
{
    protected override async Task<decimal> Calculate(decimal total) => total switch
    {
        <= 16500 => 0,
        <= 35300 => (total - 16500) * 0.2m,
        <= 70044 => (total - 35300) * 0.4m + 3760,
        _ => (total - 70044) * 0.45m + 16208
    };
}
