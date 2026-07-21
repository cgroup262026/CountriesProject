using System.Net.Http;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using CountriesProject.Models;
using CountriesProject.DAL;
using CountriesProject.DTOs;
public class CountriesApiService
{
    private readonly HttpClient _httpClient;
    private readonly DBservices _db;

    // בנאי שמקבל את אובייקט החיבור לרשת ואת הגישה ל-DB שלנו
    public CountriesApiService(HttpClient httpClient, DBservices db)
    {
        _httpClient = httpClient;
        _db = db;
    }

    // פונקציה ראשית שהקונטרולר של המנהל יפעיל
    public async Task ImportCountriesAsync()
    {
        // 1. משיכת הנתונים מה-API (של Countries.dev)
        string apiUrl = "https://countries.dev/countries?fields=name,alpha2Code,alpha3Code,capital,subregion,region,population,area,borders,flags,currencies,languages"; // נתיב לדוגמה
        var response = await _httpClient.GetAsync(apiUrl);
        response.EnsureSuccessStatusCode(); // מוודא שחזר 200 OK

        string jsonString = await response.Content.ReadAsStringAsync();

        // 2. המרה של ה-JSON לרשימה של מדינות (C#)
        // כאן נצטרך להתאים את ההמרה בדיוק למבנה של countries.dev
        List<CountryDTO> countries = JsonSerializer.Deserialize<List<CountryDTO>>(jsonString);

        foreach (var dto in countries)
        {
            // 3. יצירת אובייקט Country רגיל והעברת הנתונים מה-DTO אליו
            Country newCountry = new Country
            {
                CountryName = dto.countryName,
                Alpha2Code = dto.alpha2Code,
                Alpha3Code = dto.alpha3Code,
                Capital = dto.capital,
                // שימו לב: כאן תצטרכו להחליט לאיזה שדה ב-Country להכניס את ה-region (למשל לשדה זמני שייצרנו קודם או ישר לטפל ב-ID)
                SubRegion = dto.subregion,
                Population = dto.population,
                Area = dto.area,
                FlagUrl = dto.flags?.Svg,
                // --- המרת המטבעות ---
                // בודקים קודם שהרשימה מה-API היא לא null כדי למנוע שגיאות הקורסות את התוכנית
                Currencies = dto.currencies != null ? dto.currencies.Select(c => new Currency
                {
                    CurrencyCode = c.currencyCode,
                    CurrencyName = c.currencyName,
                    Symbol = c.symbol

                }).ToList() : new List<Currency>(), // אם אין מטבעות, ניצור רשימה ריקה

                // --- המרת השפות ---
                Languages = dto.languages != null ? dto.languages.Select(l => new Language
                {
                    LanguageCode = l.languageCode,
                    LanguageName = l.languageName

                }).ToList() : new List<Language>()
            };

            // 4. עכשיו הפונקציה תקבל את האובייקט שהיא יודעת לעבוד איתו
            SaveCountryToDatabase(newCountry);
        }
    }

    // פונקציית עזר ששומרת מדינה בודדת דרך ה-DAL
    private void SaveCountryToDatabase(Country country)
    {
        // קריאה ל-DbHelper שיפעיל את sp_UpsertRegion כדי לקבל RegionID

        // קריאה ל-DbHelper שיפעיל את sp_UpsertCountry עם נתוני המדינה וה-RegionID

        // לולאות לשמירת שפות, מטבעות וגבולות של אותה מדינה דרך ה-SPs המתאימים
    }
}
