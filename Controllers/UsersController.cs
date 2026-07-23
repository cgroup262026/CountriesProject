using Microsoft.AspNetCore.Mvc;
using CountriesProject.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CountriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            int userId = user.Register();

            if (userId > 0)
                return Ok(userId);

            return BadRequest("Registration failed.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            User user = CountriesProject.Models.User.Login(login.Email, login.Password);
            if (user == null) return Unauthorized();
            return Ok(user);
        }
    }
}
