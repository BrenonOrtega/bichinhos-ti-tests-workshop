using System.Reflection;
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

        services.AddCountriesTaxCalculators();
        
        return services;
    }

    public static IServiceCollection AddCountriesTaxCalculators(this IServiceCollection services)
    {
        var calculators = Assembly
        .GetExecutingAssembly()
        .GetImplementationsOf<ILocationTaxCalculator>();

        foreach (var calculator in calculators)
        {
            services.AddScoped(typeof(ILocationTaxCalculator), calculator);
        }

        return services;
    }

}
