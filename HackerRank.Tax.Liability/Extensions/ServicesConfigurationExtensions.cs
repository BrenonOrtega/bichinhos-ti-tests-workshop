using HackerRank.Tax.Liability.App;
using HackerRank.Tax.Liability.Infrastructure;

namespace HackerRank.Tax.Liability.Extensions;

public static class ServicesConfigurationExtensions
{
    public static IServiceCollection AddCalculatorServices(this IServiceCollection services)
    {
        services.AddSingleton<IWebhookAddressRepository, WebhookAddressRepository>();

        services.AddTransient<ICalculatorApp, Calculator>()
                .Decorate<ICalculatorApp, CalculatorWithPostWebhook>()
                ;

        return services;
    }
}
