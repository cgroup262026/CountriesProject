using CountriesProject.Models;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CountriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        [HttpPost("import")]
        public async Task<IActionResult> ImportCountries()
        {
            int count = await Country.ImportAllCountries();
            return Ok(new { ImportedCountries = count });
        }

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            List<Country> countries = Country.GetAllCountries();
            return Ok(countries);
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
            int affectedRows = country.Insert();

            if (affectedRows > 0) return Ok(country);
            return BadRequest("Failed to add country.");
        }

        [HttpPut("{alpha3Code}")]
        public IActionResult UpdateCountry(string alpha3Code, [FromBody] Country country)
        {
            country.Alpha3Code = alpha3Code;
            int affectedRows = country.Update();

            if (affectedRows > 0) return Ok(country);
            return NotFound("Country not found.");
        }

        [HttpDelete("{alpha3Code}")]
        public IActionResult DeleteCountry(string alpha3Code)
        {
            int affectedRows = Country.Delete(alpha3Code);

            if (affectedRows > 0) return Ok("Country deleted successfully.");
            return NotFound("Country not found.");
        }

        [HttpGet("search")]
        public IActionResult SearchCountries(string name = null, string region = null, string language = null, string currency = null, long? minPopulation = null, long? maxPopulation = null, double? minArea = null, double? maxArea = null, string sortBy = "name", string sortDirection = "asc")
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
