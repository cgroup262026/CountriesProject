using Microsoft.AspNetCore.Mvc;
using CountriesProject.Models;
using System.Collections.Generic;
using System;

namespace CountriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> users = CountriesProject.Models.User.GetAllUsers();
            return Ok(users);
        }

        // GET api/<UsersController>/5
        [HttpGet("{userId}")]
        public User Get(int userId)
        {
            return CountriesProject.Models.User.GetUserById(userId);
        }

        // POST api/<UsersController>/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            int userId = user.Register();

            if (userId > 0)
            {
                return Ok(userId);
            }

            return BadRequest("Registration failed.");
        }

        // POST api/<UsersController>/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            User user = CountriesProject.Models.User.Login(login.Email, login.Password);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            return Ok(user);
        }

        // PUT api/<UsersController>/
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User updatedUser)
        {
            updatedUser.UserId = id;

            int affectedRows = updatedUser.UpdateUser();

            if (affectedRows > 0)
            {
                return Ok(updatedUser);
            }

            return BadRequest("Update failed.");
        }

        // PUT api/<UsersController>/lock/
        [HttpPut("lock/{id}")]
        public IActionResult UpdateLockStatus(int id, [FromBody] bool isLocked)
        {
            int affectedRows = CountriesProject.Models.User.UpdateLockStatus(id, isLocked);

            if (affectedRows > 0)
            {
                return Ok(new { Message = "User lock status updated successfully." });
            }

            return BadRequest("Failed to update lock status.");
        }

        [HttpGet("hobbies")]
        public List<string> GetAllHobbies()
        {
            return CountriesProject.Models.User.GetAllHobbies();
        }

        // PUT api/<UsersController>/
        [HttpPut("{id}/hobbies")]
        public IActionResult UpdateHobbies(int id, [FromBody] List<string> hobbies)
        {
            try
            {
                CountriesProject.Models.User.UpdateUserHobbies(id, hobbies);
                return Ok(new { Message = "Hobbies updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update hobbies: {ex.Message}");
            }
        }

        [HttpGet("{userId}/hobbies")]
        public List<string> GetUserHobbies(int userId)
        {
            return CountriesProject.Models.User.GetUserHobbies(userId);
        }

        // DELETE api/<UsersController>/
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int affectedRows = CountriesProject.Models.User.DeleteOrLockUser(id);

            if (affectedRows > 0)
            {
                return Ok("User deleted successfully.");
            }

            return BadRequest("Failed to delete user.");
        }

        [HttpGet("{userId}/total-score")]
        public int GetUserTotalScore(int userId)
        {
            return CountriesProject.Models.User.GetUserTotalScore(userId);
        }

        [HttpPost("{userId}/saved-countries/{alpha3Code}/{listType}")]
        public int AddSavedCountry(int userId, string alpha3Code, string listType)
        {
            return CountriesProject.Models.User.AddSavedCountry(userId, alpha3Code, listType);
        }

        [HttpDelete("{userId}/saved-countries/{alpha3Code}")]
        public int DeleteSavedCountry(int userId, string alpha3Code)
        {
            return CountriesProject.Models.User.DeleteSavedCountry(userId, alpha3Code);
        }

        [HttpGet("{userId}/saved-countries/{listType}")]
        public List<Country> GetSavedCountries(int userId, string listType)
        {
            return CountriesProject.Models.User.GetSavedCountries(userId, listType);
        }
    }
}