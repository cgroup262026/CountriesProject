using CountriesProject.Models;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CountriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            List<Country> countries = Country.GetAllCountries();
            return Ok(countries);
        }

        [HttpGet("currencies")]
        public IActionResult GetAllCurrencies()
        {
            List<string> currencies = Country.GetAllCurrencies();
            return Ok(currencies);
        }

        [HttpGet("{alpha3Code}")]
        public IActionResult GetCountryByCode(string alpha3Code)
        {
            Country country = Country.GetCountryByCode(alpha3Code);

            if (country == null) return NotFound("Country not found.");
            return Ok(country);
        }

        //POST api/<CountriesController>
        [HttpPost("add")]
        public IActionResult AddCountry([FromBody] Country country)
        {
            int numEffected = country.Insert();

            if (numEffected > 0) return Ok(country);
            return BadRequest("Failed to add country.");
        }

        [HttpPut("{alpha3Code}")]
        public IActionResult UpdateCountry(string alpha3Code, [FromBody] Country country)
        {
            country.Alpha3Code = alpha3Code;
            int numEffected = country.Update();

            if (numEffected > 0) return Ok(country);
            return NotFound("Country not found.");
        }

        [HttpDelete("{alpha3Code}")]
        public IActionResult DeleteCountry(string alpha3Code)
        {
            int numEffected = Country.Delete(alpha3Code);

            if (numEffected > 0) return Ok("Country deleted successfully.");
            return NotFound("Country not found.");
        }

        [HttpGet("search")]
        public IActionResult SearchCountries(string? name = null, string? region = null, string? language = null, string? currency = null, long? minPopulation = null, long? maxPopulation = null, double? minArea = null, double? maxArea = null, string sortBy = "name", string sortDirection = "asc")
        {
            List<Country> countries = Country.SearchCountries(name, region, language, currency, minPopulation, maxPopulation, minArea, maxArea, sortBy, sortDirection);
            return Ok(countries);
        }

        [HttpGet("memory-game")]
        public List<Country> GetMemoryGameCountries()
        {
            return Country.GetMemoryGameCountries();
        }
    }
}
