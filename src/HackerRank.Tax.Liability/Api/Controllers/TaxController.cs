using System.ComponentModel.DataAnnotations;
using HackerRank.Tax.Liability.Api.Requests;
using HackerRank.Tax.Liability.App;
using Microsoft.AspNetCore.Mvc;

namespace HackerRank.Tax.Liability.Api.Controllers;

[ApiController]
[Route("[Controller]")]
public class TaxesController : ControllerBase
{
    private readonly ICalculatorApp calculator;
    public TaxesController(ICalculatorApp calculator) => this.calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));

    [HttpPost]
    public async Task<ActionResult<IncomeCalculation>> GetCalculation([Required] IncomeCalculationRequest request)
    {
        var result = await calculator.Calculate(request.Country, request.HourlyRate, request.WorkedHours);

        return Ok(result);
    }
}