using CountriesProject.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CountriesProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllReviews()
        {
            return Ok(Review.GetAllReviews());
        }

        [HttpGet("country/{alpha3Code}")]
        public IActionResult GetReviewsByCountry(string alpha3Code)
        {
            return Ok(Review.GetReviewsByCountry(alpha3Code));
        }

        [HttpGet("user/{userId}")]
        public IActionResult GetReviewsByUser(int userId)
        {
            return Ok(Review.GetReviewsByUser(userId));
        }

        [HttpPost]
        public int Post([FromBody] Review review)
        {
            return review.Insert();
        }

        [HttpPut("{reviewId}")]
        public int Put(int reviewId, [FromBody] Review review)
        {
            review.ReviewId = reviewId;
            return review.Update();
        }

        [HttpDelete("{reviewId}/user/{userId}")]
        public int Delete(int reviewId, int userId)
        {
            return Review.Delete(reviewId, userId);
        }
    }
}
