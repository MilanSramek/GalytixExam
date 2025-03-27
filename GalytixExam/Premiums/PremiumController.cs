using Application;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace GalytixExam.Premiums;

[Route("server/api/gwp")]
[ApiController]
public sealed class PremiumController : ControllerBase
{
    [HttpPost("avg")]
    public async Task<IActionResult> GetAveragePremiumsAsync(
        [FromServices] IAveragePremiumProvider averagePremiumProvider,
        [FromServices] ILogger<PremiumController> logger,
        [FromBody] GetAveragePremiumRequest requestInput,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(averagePremiumProvider);
        ArgumentNullException.ThrowIfNull(requestInput);

        try
        {
            var input = new GetAveragePremiumInput(
                new Country(requestInput.Country),
                requestInput.LineOfBusinessItems?
                    .Select(_ => new LineOfBusiness(_))
                    .ToArray());

            IEnumerable<AveragePremiumModel> averagePremiums = await averagePremiumProvider
                .GetAveragePremiumsAsync(input, cancellationToken);

            var response = averagePremiums
                .ToDictionary(
                    _ => _.LineOfBusiness.ToString(),
                    _ => _.Value);

            return Ok(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred when processing post request at route {route}", "avg");
            return StatusCode(500, "Internal server error");
        }
    }
}
