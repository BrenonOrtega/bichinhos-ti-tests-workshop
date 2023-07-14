
using System.Reflection;

namespace HackerRank.Tax.Liability.App;

public class Calculator : ICalculatorApp
{
        static readonly IEnumerable<Type?> calculatorImplementations = Assembly
            .GetExecutingAssembly()
            .GetImplementationsOf<ILocationTaxCalculator>();
            
        public async Task<IncomeCalculation> Calculate(string country, decimal hourlyRate, int hours)
        {
            ILocationTaxCalculator calculator = GetCalculatorInstance(country);
            var calculation = calculator.Calculate(hourlyRate, hours);
            return calculation;
        }

        private static ILocationTaxCalculator GetCalculatorInstance(string country)
        {
            var calculatorType = GetLocationCalculatorType(country);
            return Activator.CreateInstance(calculatorType) as ILocationTaxCalculator;
        }

    private static Type GetLocationCalculatorType(string country)
    {
        var type = calculatorImplementations.FirstOrDefault(IsCalculatorFor(country));

        _ = type ?? throw new NotImplementedException($"There`s no implementation of tax calculator for country {country}");
        
        return type;
    }

    private static Func<Type, bool> IsCalculatorFor(string country)
        => type => type.Name.ToUpper().StartsWith(country?.ToUpper() ?? "");

}
