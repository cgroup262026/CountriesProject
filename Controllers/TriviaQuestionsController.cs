using CountriesProject.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CountriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriviaQuestionsController : ControllerBase
    {
        [HttpGet]
        public List<TriviaQuestion> GetRandomTriviaQuestions()
        {
            return TriviaQuestion.GetRandomTriviaQuestions();
        }
    }
}
