using CountriesProject.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CountriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedCountriesController : ControllerBase
    {
        [HttpPost("{userId}/{alpha3Code}/{listType}")]
        public int AddSavedCountry(int userId, string alpha3Code, string listType)
        {
            return CountriesProject.Models.User.AddSavedCountry(userId, alpha3Code, listType);
        }

        [HttpDelete("{userId}/{alpha3Code}")]
        public int DeleteSavedCountry(int userId, string alpha3Code)
        {
            return CountriesProject.Models.User.DeleteSavedCountry(userId, alpha3Code);
        }

        [HttpGet("{userId}/{listType}")]
        public List<Country> GetSavedCountries(int userId, string listType)
        {
            return CountriesProject.Models.User.GetSavedCountries(userId, listType);
        }
    }
}
