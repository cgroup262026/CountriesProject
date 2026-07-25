using CountriesProject.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CountriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreferencesController : ControllerBase
    {
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

        [HttpGet("languages")]
        public IActionResult GetAllLanguages()
        {
            List<string> languages = UserLanguage.GetAllLanguages();
            return Ok(languages);
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
