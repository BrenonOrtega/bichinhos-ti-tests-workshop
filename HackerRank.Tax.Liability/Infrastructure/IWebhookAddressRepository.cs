namespace HackerRank.Tax.Liability.Infrastructure;

public interface IWebhookAddressRepository
{
    IEnumerable<string> getUrls();
    bool TryAddUrl(string url);
}
