using CountriesProject.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CountriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriviaResultsController : ControllerBase
    {
        [HttpPost]
        public int Post([FromBody] TriviaResult result)
        {
            return result.Insert();
        }
    }
}
