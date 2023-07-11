using System.Net.Http.Json;
using HackerRank.Tax.Liability.Infrastructure;

namespace HackerRank.Tax.Liability.App;

public class CalculatorWithPostWebhook : ICalculatorApp
{
    private readonly ICalculatorApp next;
    public static HttpClient client = new HttpClient();
    private readonly IWebhookAddressRepository urlRepo;

    public CalculatorWithPostWebhook(ICalculatorApp next, IWebhookAddressRepository repo) 
        => (this.next,this.urlRepo) = 
            (next ?? throw new ArgumentNullException(nameof(next)), repo ?? throw new ArgumentNullException(nameof(repo)));

    public async Task<IncomeCalculation> Calculate(string country, decimal hourlyRate, int hours)
    {
        var calculation = await next.Calculate(country, hourlyRate, hours);

        await PostMessage(calculation, country, hourlyRate, hours);

        return calculation;
    }

    private async Task PostMessage(IncomeCalculation calculation, string country, decimal hourlyRate, int hours)
    {
        var response = JsonContent.Create(calculation);
        var tasks = urlRepo.getUrls().Select(url => client.PostAsync(url, response));

        await Task.WhenAll(tasks);
    }
}
