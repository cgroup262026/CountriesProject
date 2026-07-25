using Microsoft.AspNetCore.Mvc;
using CountriesProject.Models;
using System.Collections.Generic;

namespace CountriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        // GET api/Admin/users
        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            List<User> users = CountriesProject.Models.User.GetAllUsers();
            return Ok(users);
        }

        // PUT api/Admin/users/5/lock
        [HttpPut("users/{id}/lock")]
        public IActionResult UpdateLockStatus(int id, [FromBody] bool isLocked)
        {
            int affectedRows = CountriesProject.Models.User.UpdateLockStatus(id, isLocked);

            if (affectedRows > 0)
            {
                return Ok(new { Message = "User lock status updated successfully." });
            }
            return BadRequest("Failed to update lock status.");
        }

        // DELETE api/Admin/users/5
        [HttpDelete("users/{id}")]
        public IActionResult DeleteUser(int id)
        {
            int affectedRows = CountriesProject.Models.User.DeleteOrLockUser(id);

            if (affectedRows > 0)
            {
                return Ok(new
                {
                    message = "User deleted successfully."
                });
            }

            return BadRequest(new
            {
                message = "Failed to delete user."
            });
        }

        [HttpGet("statistics")]
        public IActionResult GetStatistics()
        {
            AdminStatistics statistics = AdminStatistics.GetStatistics();
            return Ok(statistics);
        }
    }
}