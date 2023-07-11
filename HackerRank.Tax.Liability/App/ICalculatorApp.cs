namespace HackerRank.Tax.Liability.App;

public interface ICalculatorApp
{
    Task<IncomeCalculation> Calculate(string country, decimal hourlyRate, int hours);
}
