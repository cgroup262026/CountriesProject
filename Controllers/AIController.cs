using CountriesProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CountriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController : ControllerBase
    {
        private readonly AiService aiService;

        public AiController(AiService aiService)
        {
            this.aiService = aiService;
        }

        [HttpGet("country-fact")]
        public async Task<IActionResult> GetCountryFact([FromQuery] string? countryName, CancellationToken cancellationToken)
        {
            try
            {
                string fact = await aiService.GenerateCountryFactAsync(countryName ?? "", cancellationToken);
                return Ok(new { fact });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, new { message = ex.Message });
            }
            catch (OperationCanceledException)
            {
                return StatusCode(StatusCodes.Status504GatewayTimeout, new { message = "The AI service took too long to respond." });
            }
            catch (HttpRequestException)
            {
                return StatusCode(StatusCodes.Status502BadGateway, new { message = "The AI service is temporarily unavailable." });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An unexpected error occurred while generating the fact." });
            }
        }
    }
}