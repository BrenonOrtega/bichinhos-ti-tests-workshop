namespace HackerRank.Tax.Liability.Infrastructure;

class WebhookAddressRepository : IWebhookAddressRepository
{
    private static readonly HashSet<string> urls = new()
    {
        "https://bin.webhookrelay.com/v1/webhooks/eafa8e8c-caa0-4cad-8a83-1dc3def84b50",
    };

    public IEnumerable<string> getUrls() => urls;

    public bool TryAddUrl(string url) => urls.Add(url);
}