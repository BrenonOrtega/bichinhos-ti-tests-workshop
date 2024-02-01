public interface ILocationTaxCalculator
{
    Task<IncomeCalculation> Calculate(decimal hourlyRate, int workedHours);
}
