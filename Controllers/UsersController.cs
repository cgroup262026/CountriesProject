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
        // 1. קבלת כל המשתמשים (לשימוש המנהל)
        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<User> users = CountriesProject.Models.User.GetAllUsers();
            return Ok(users);
        }

        // 2. קבלת משתמש ספציפי לפי מזהה (לתצוגת הפרופיל שלו)
        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            User user = CountriesProject.Models.User.GetUserById(id);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        // 3. הרשמה (יצירת משתמש חדש)
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

        // 4. התחברות
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

        // 5. עדכון פרטי משתמש בסיסיים (שם, תאריך לידה, מגדר, תמונה)
        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User updatedUser)
        {
            updatedUser.UserId = id; // מוודאים שהמזהה בפנייה תואם לאובייקט

            int affectedRows = updatedUser.UpdateUser();

            if (affectedRows > 0)
            {
                return Ok(updatedUser);
            }

            return BadRequest("Update failed.");
        }

        // 6. עדכון סטטוס נעילה של משתמש (לשימוש המנהל)
        // PUT api/<UsersController>/lock/5
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

        // 7. עדכון רשימת התחביבים של המשתמש (מחיקה והוספה מחדש בפרוצדורה אחת)
        // PUT api/<UsersController>/5/hobbies
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

        // 8. מחיקת משתמש פיזית ממסד הנתונים
        // DELETE api/<UsersController>/5
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
    }
}