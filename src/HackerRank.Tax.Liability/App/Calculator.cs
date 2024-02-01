
using System.Reflection;

namespace HackerRank.Tax.Liability.App;

public class Calculator : ICalculatorApp
{
    private readonly IEnumerable<ILocationTaxCalculator> calculators;

    public Calculator(IEnumerable<ILocationTaxCalculator> calculators)
    {
        this.calculators = calculators ?? throw new ArgumentNullException(nameof(calculators));
    }

    public async Task<IncomeCalculation> Calculate(string country, decimal hourlyRate, int hours)
    {
        ILocationTaxCalculator calculator = GetCalculatorInstance(country);
        var calculation = await calculator.Calculate(hourlyRate, hours);
        return calculation;
    }

    private ILocationTaxCalculator GetCalculatorInstance(string country)
    {
        var calculator = calculators.FirstOrDefault(IsCalculatorFor(country));

        _ = calculator ?? throw new NotImplementedException($"There`s no implementation of tax calculator for country {country}");

        return calculator;
    }

    private static Func<ILocationTaxCalculator, bool> IsCalculatorFor(string country)
        => calculator => calculator.GetType().Name.ToUpper().StartsWith(country?.ToUpper() ?? "");
}
