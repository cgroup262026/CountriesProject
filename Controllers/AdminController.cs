using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CountriesProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly CountriesApiService _countriesService;

        // הזרקת השירות (Dependency Injection) לקונטרולר
        public AdminController(CountriesApiService countriesService)
        {
            _countriesService = countriesService;
        }

        // יצירת ראוט להפעלת הייבוא (Post לרוב מתאים לפעולות שמשנות נתונים בשרת)
        [HttpPost("import-countries")]
        public async Task<IActionResult> TriggerImport()
        {
            try
            {
                // קריאה לפונקציה שלך
                await _countriesService.ImportCountriesAsync();

                return Ok("Complete.");
            }
            catch (System.Exception ex)
            {
                // במקרה של שגיאה (למשל אם ה-API למטה או שה-JSON לא תקין)
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}