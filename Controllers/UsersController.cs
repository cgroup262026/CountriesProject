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
            int numEffected = user.Register();

            if (numEffected > 0)
            {
                return Ok(numEffected);
            }

            return BadRequest("Registration failed.");
        }

        // POST api/<UsersController>/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest login)
        {
            try
            {
                User user = CountriesProject.Models.User.Login(login.Email, login.Password);

                if (user == null)
                {
                    return Unauthorized("Invalid email or password.");
                }

                user.PasswordHash = null;// Clear the password hash before returning the user object for security reasons
                return Ok(user);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<UsersController>/
        [HttpPut("{id}")]
        public IActionResult Put(
            int id,
            [FromBody] User updatedUser)
        {
            try
            {
                User? savedUser = CountriesProject.Models.User.UpdateProfile(id, updatedUser);

                if (savedUser == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(savedUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{userId}/total-score")]
        public int GetUserTotalScore(int userId)
        {
            return CountriesProject.Models.User.GetUserTotalScore(userId);
        }
    }
}