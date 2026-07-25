using CountriesProject.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CountriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        [HttpGet("languages")]
        public IActionResult GetAllLanguages()
        {
            List<string> languages = UserLanguage.GetAllLanguages();
            return Ok(languages);
        }
    }
}
