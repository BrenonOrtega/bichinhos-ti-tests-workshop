using System.Reflection;
using HackerRank.Tax.Liability.App;
using HackerRank.Tax.Liability.Extensions;
using HackerRank.Tax.Liability.Infrastructure;

namespace HackerRank.Tax.Liability;
public class ConsoleApplication
{
    private static IServiceCollection _services;
    public static IServiceProvider CurrentServiceProvider { get; private set; }
    public static ICalculatorApp GetApp()
    {
        CurrentServiceProvider = Services.BuildServiceProvider();

        return CurrentServiceProvider.GetRequiredService<ICalculatorApp>();
    }

    public static IServiceCollection Services =>
        _services ??= new ServiceCollection().AddCalculatorServices()
            ;

    public static ICalculatorApp GetAppWithoutDependencyInjectionContainer()
    {
        var originalCalculator = new CalculatorWithoutDI();
        var webhookRepo = new WebhookAddressRepository();
        var decoratedCalculator = new CalculatorWithPostWebhook(originalCalculator, webhookRepo);

        return decoratedCalculator;
    }

    public static async Task Run()
    {
        var referenceCountry = GetInput($"Input the desired country: {GetExistingCalculators()}");
        var hourlyRate = TryGetDecimal("What is the hourly rate of the worker? (enter only numbers please).");
        var workedHours = TryGetDecimal("How many hours have this employee worked? (enter only numbers).");

        var calculation = await GetAppWithoutDependencyInjectionContainer()
            .Calculate(referenceCountry, hourlyRate, (int)workedHours);

        WriteTaxOutput(referenceCountry, hourlyRate, workedHours, calculation);
    }

    private static string GetExistingCalculators()
    {
        var names = Assembly.GetExecutingAssembly()
            .GetTypes().GetImplementationsOf<ILocationTaxCalculator>()
            .Select(type => type.Name.Replace("TaxCalculator", ""));

        return names.Aggregate("", (initial, actual) => $"{initial} | {actual}");
    }

    private static void WriteTaxOutput(string referenceCountry, decimal hourlyRate, decimal workedHours, IncomeCalculation calculation)
    {
        Console.WriteLine($"The tax liability for {referenceCountry}.");
        Console.WriteLine($"Hourly rate of {hourlyRate:C2}.");
        Console.WriteLine($"working {workedHours} hrs.");
        Console.WriteLine($"Tax liability {calculation.TotalTaxes:C2}");
        Console.WriteLine($"Total gross income {calculation.GrossIncome:C2}.");
        Console.WriteLine($"Net Value {calculation.NetValue:C2}.");
    }

    private static decimal TryGetDecimal(string message)
    {
        var stringHourlyRate = GetInput(message);
        try
        {
            var decimalValue = Convert.ToDecimal(stringHourlyRate);
            return decimalValue;
        }
        catch
        {
            Console.WriteLine("Error when converting value to decimal number. Please enter only numeric digits");
            return TryGetDecimal(message);
        }
    }

    private static string GetInput(string message)
    {
        Console.WriteLine(message);
        var input = Console.ReadLine();
        return input;
    }
}