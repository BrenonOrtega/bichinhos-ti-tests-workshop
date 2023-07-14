using HackerRank.Tax.Liability.App;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HackerRank.Tax.Liability;

public class TestRunner
{
    [Theory]
    [InlineData("Italy", 20.5, 40, 240.83)]
    [InlineData("Germany", 25, 35, 275.05)]
    [InlineData("Ireland", 80, 40, 1145.54)]
    //Didn t make the calculations by myself for each case, just making sure its passing.
    public async Task Should_Calculate_Based_On_Countries(string country, decimal hourlyRate, int hours, decimal expected)
    {
        var calculator = ConsoleApplication.GetApp();
        var result = await calculator.Calculate(country, hourlyRate, hours);

        Assert.Equal(expected, result.TotalTaxes, precision: 2);
    }

    [Fact]
    public async Task Not_Implemented_Calculator_Should_Throw()
    {
        var invalidCountry = "bryanland";
        Program.ConfigureApplication(WebApplication.CreateBuilder());
        var calculator = Program.ServiceProvider.GetRequiredService<ICalculatorApp>();

        var throwAction = () => calculator.Calculate(invalidCountry, 20, 20);

        await Assert.ThrowsAsync<NotImplementedException>(throwAction);
    }
}
