abstract class BaseTaxCalculator : ILocationTaxCalculator
{
    protected abstract Task<decimal> Calculate(decimal total);
    private string CountryName => this.GetType().Name.Replace("TaxCalculator", string.Empty);

    public async Task<IncomeCalculation> Calculate(decimal hourlyRate, int workedHours)
    {
        var weeklyIncome = hourlyRate * workedHours;
        var total = weeklyIncome * 52;
        var annualLiability = await Calculate(total);
        var liability = annualLiability / 52;
        var netValue = weeklyIncome - liability;

        return new (liability, weeklyIncome, netValue, CountryName);
    }
}
