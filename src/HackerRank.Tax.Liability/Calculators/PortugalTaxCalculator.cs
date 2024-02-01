namespace HackerRank.Tax.Liability.Calculators;

internal class PortugalTaxCalculator : BaseTaxCalculator
{
    private const string RequestUri = "api/v1/calculator";
    private readonly HttpClient client;

    public PortugalTaxCalculator(IHttpClientFactory factory)
    {
        this.client = factory.CreateClient("rust-service") ?? throw new ArgumentNullException(nameof(client));
    }

    protected override async Task<decimal> Calculate(decimal total)
    {
        var response = await client.GetAsync($"{RequestUri}/{total}");
        var content = await response.Content.ReadFromJsonAsync<CalculationResponse>();

        return content.net_value;
    }

    public struct CalculationResponse
    {
        public decimal net_value { get; set; }
    }
}
