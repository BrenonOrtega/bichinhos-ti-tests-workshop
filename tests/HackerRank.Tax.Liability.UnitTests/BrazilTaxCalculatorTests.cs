using Xunit;

namespace HackerRank.Tax.Liability.UnitTests
{
    public class ItalyTaxCalculatorTests
    {
        [Fact]
        public async Task Should_Calculate_Tax_Correctly()
        {
            var hourlyRate = 20.5m;
            var hours = 40;
            var expected = 240.83m;

            var calculator = new ItalyTaxCalculator();

            var result = await calculator.Calculate(hourlyRate, hours);

            Assert.Equal(expected, result.TotalTaxes, 2);
        }
    }
}