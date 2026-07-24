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
        [HttpPost]
        [Route("api/countries/add")]
        public IActionResult AddCountry([FromBody] Country country)
        {
            int affectedRows = country.Insert();

            if (affectedRows > 0) return Ok(country);
            return BadRequest("Failed to add country.");
        }

        // PUT api/<CountriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CountriesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("memory-game")]
        public List<Country> GetMemoryGameCountries()
        {
            return Country.GetMemoryGameCountries();
        }
    }
}
