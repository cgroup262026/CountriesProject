using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CountriesProject.Models
{
    public class Country
    {
        public string Alpha3Code { get; set; }
        public string Alpha2Code { get; set; }
        public string CountryName { get; set; }
        public string Capital { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public string Subregion { get; set; }
        public long? Population { get; set; }
        public double? Area { get; set; }
        public string FlagUrl { get; set; }

        // רשימות מקושרות למדינה
        public List<Currency> Currencies { get; set; } = new List<Currency>();
        public List<Language> Languages { get; set; } = new List<Language>();
        public List<Country> Borders { get; set; } = new List<Country>(); // כל מדינות הגבול כאובייקטים של מדינה
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
