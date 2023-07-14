# Country Income Tax Calculator

## Objective

Simple .NET 7 Income tax calculator Developed based on a HackerRank challenge. Having the core of the application using native Core .NET features. 

The challenge was to implement a calculator that would take the country name (case-insensitive), worked hours and hourly rate and based on given rules would return how much is the net value of your salary, debt taxes and gross income for that payslip.

The challenge asked to implement for 3 countries, 'Ireland', 'Germany' and 'Italy'.
As this challenge was developed the extensibility and simplicity of the solution should always be kept in focus. (Even though I decided to experiment decorating and posting to a webhook receiver, as a way to keep learning about webhooks).

In order to achieve that, I've developed using name conventions for the calculator.
As long as a new calculator was needed, we could just implement a <Country>TaxCalculator and this method would scan all calculators and return an instance of this calculators, if exists one for this country.

```csharp
  // Calculator.cs
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
```

This applies the strategy pattern and facade pattern.
We have a strategy conforming to a common interface and can be changed at runtime to handle the use cases.
in this case the ILocationTaxCalculator that has the Calculate(decimal, decimal) method, receiving the hourlyRate and workedHours.
Every class can have its own implementation and rules for calculating the netValue of a payslip and it will be changed in runtime.

The facade pattern is implemented having the class Calculator that handles the logic behind of this classes change in runtime and in case of adding new methods for other type of calculations, would be the entrypoint for external applications, no need to expose interfaces and other stuff to the world, right?

There is always something we could improve, but I feel like achieving the objective of this challenge with the actual implementation.

## How to run

simply navigate to the HackerRank.Tax.Liability folder and run the following command
```cmd
    dotnet run [--console]
```
The parameter --console would not start the webapi and run the console version instead.

Enjoy! :D