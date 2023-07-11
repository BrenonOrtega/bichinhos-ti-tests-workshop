public interface ILocationTaxCalculator
{
    IncomeCalculation Calculate(decimal hourlyRate, int workedHours);
}
