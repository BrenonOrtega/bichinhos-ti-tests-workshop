using HackerRank.Tax.Liability.Calculators;
using NSubstitute;
using Xunit;

namespace tests.HackerRank.Tax.Liability.IntegrationTests.Calculators;

public class PortugalTaxCalculatorTests
{
    [Fact]
    public async Task When_Sending_Total_Income_Get_Correct_Value()
    {
        var hourlyRate = 50;
        var workedHours = 145;

        var expected = 6162.5m; 

        var factory = Substitute.For<IHttpClientFactory>();
        using var client = new HttpClient();

        factory.CreateClient(Arg.Any<string>()).Returns(client);

        var calculator = new PortugalTaxCalculator(factory);

        var result = await calculator.Calculate(hourlyRate, workedHours);

        Assert.Equal(expected, result.NetValue, 2);
    }

    [Fact]
    public async Task When_Sending_Total_Income_Should_Calculate_Correctly()
    {
        var hourlyRate = 100;
        var workedHours = 40;

        var expected = 3400; 

        var factory = Substitute.For<IHttpClientFactory>();
        using var client = new HttpClient();

        factory.CreateClient(Arg.Any<string>()).Returns(client);
        var calculator = new PortugalTaxCalculator(factory);

        var result = await calculator.Calculate(hourlyRate, workedHours);

        Assert.Equal(expected, result.NetValue, 2);
        Assert.Equal(350, result.TotalTaxes);
    }
}
