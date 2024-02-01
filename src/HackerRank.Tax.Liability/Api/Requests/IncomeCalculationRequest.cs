namespace HackerRank.Tax.Liability.Api.Requests;

public record IncomeCalculationRequest(string Country, int WorkedHours, decimal HourlyRate);