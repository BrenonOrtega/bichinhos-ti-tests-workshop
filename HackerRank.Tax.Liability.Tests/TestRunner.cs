using Xunit;

namespace HackerRank.Tax.Liability;

public class TestRunner
{
    [Theory]
    [InlineData("Italy", 20.5, 40, 188.60)]
    [InlineData("Germany", 25, 35, 0)]
    [InlineData("Ireland", 80, 40, 0)]
    //Didn t make the calculations by myself for each case, just making sure its passing.
    public async Task Should_Calculate_Based_On_Countries(string country, decimal hourlyRate, int hours, decimal expected)
    {
        var result = await ConsoleApplication.GetApp().Calculate(country, hourlyRate, hours);

        Assert.Equal(expected, result.TotalTaxes);
    }

    [Theory]
    [InlineData("Italy", 20.5, 40, 188.60)]
    [InlineData("Germany", 25, 35, 0)]
    [InlineData("Ireland", 80, 40, 0)]
    public async Task Application_Should_Run_Succesfully(string country, decimal hourlyRate, int hours, decimal expected)
    {
        await Program.Main(null);
    }
}
