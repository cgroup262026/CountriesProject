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

        [HttpPut("{userId}/languages")]
        public IActionResult UpdateUserLanguages(int userId, [FromBody] List<UserLanguage> languages)
        {
            UserLanguage.UpdateUserLanguages(userId, languages);
            return Ok("User languages updated successfully.");
        }

        [HttpGet("regions")]
        public IActionResult GetAllRegions()
        {
            List<string> regions = CountriesProject.Models.User.GetAllRegions();
            return Ok(regions);
        }

        [HttpPut("{userId}/regions")]
        public IActionResult UpdateUserRegions(int userId, [FromBody] List<string> regions)
        {
            CountriesProject.Models.User.UpdateUserRegions(userId, regions);
            return Ok("User regions updated successfully.");
        }

        [HttpGet("travel-preferences")]
        public IActionResult GetAllTravelPreferences()
        {
            List<string> preferences = CountriesProject.Models.User.GetAllTravelPreferences();
            return Ok(preferences);
        }

        [HttpPut("{userId}/travel-preferences")]
        public IActionResult UpdateUserTravelPreferences(int userId, [FromBody] List<string> preferences)
        {
            CountriesProject.Models.User.UpdateUserTravelPreferences(userId, preferences);
            return Ok("User travel preferences updated successfully.");
        }
    }
}