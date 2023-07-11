using System.Text.Json.Serialization;
using HackerRank.Tax.Liability.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HackerRank.Tax.Liability;

public class Program
{
    public static async Task Main(string[] args)
    {
        if (args?.Any(x => x == "console") ?? false)
            await ConsoleApplication.Run();

        else
            await StartApi(args);
    }

    private static async Task StartApi(string[]? args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddMvc(x =>
        {
            x.EnableEndpointRouting = true;
            x.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
        })
        .AddJsonOptions(x =>
        {
            x.AllowInputFormatterExceptionMessages = true;
            x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        })
        .AddControllersAsServices()
        ;

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddControllers();
        builder.Services.AddLogging();
        builder.Services.AddResponseCompression();

        builder.Services.AddCalculatorServices();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }


}