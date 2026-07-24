using CountriesProject.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CountriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemoryGameResultsController : ControllerBase
    {
        [HttpPost]
        public int Post([FromBody] MemoryGameResult result)
        {
            return result.Insert();
        }
    }
}
