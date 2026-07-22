using CountriesProject.DAL;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace CountriesProject.Models
{
    public class Country
    {

        string alpha3Code;
        string alpha2Code;
        string countryName;
        string capital;
        int regionId;
        Region region;
        string subRegion;
        long? population;
        double? area;
        string flagUrl; 

        // רשימות מקושרות למדינה
         List<Currency> currencies  = new List<Currency>();
         List<Language> languages  = new List<Language>();
         List<Country> borders  = new List<Country>(); // כל מדינות הגבול כאובייקטים של מדינה
         List<Review> reviews  = new List<Review>();

        public Country(string alpha3Code, string alpha2Code, string countryName, string capital, int regionId, Region region, 
            string subRegion, long? population, double? area, string flagUrl, List<Currency> currencies, 
            List<Language> languages, List<Country> borders, List<Review> reviews)
        {
            Alpha3Code = alpha3Code;
            Alpha2Code = alpha2Code;
            CountryName = countryName;
            Capital = capital;
            RegionId = regionId;
            Region = region;
            SubRegion = subRegion;
            Population = population;
            Area = area;
            FlagUrl = flagUrl;
            Currencies = currencies;
            Languages = languages;
            Borders = borders;
            Reviews = reviews;
        }
        public Country() { }

        public string Alpha3Code { get => alpha3Code; set => alpha3Code = value; }
        public string Alpha2Code { get => alpha2Code; set => alpha2Code = value; }
        public string CountryName { get => countryName; set => countryName = value; }
        public string Capital { get => capital; set => capital = value; }
        public int RegionId { get => regionId; set => regionId = value; }
        public Region Region { get => region; set => region = value; }
        public string SubRegion { get => subRegion; set => subRegion = value; }
        public long? Population { get => population; set => population = value; }
        public double? Area { get => area; set => area = value; }
        public string FlagUrl { get => flagUrl; set => flagUrl = value; }
        public List<Currency> Currencies { get => currencies; set => currencies = value; }
        public List<Language> Languages { get => languages; set => languages = value; }
        public List<Country> Borders { get => borders; set => borders = value; }
        public List<Review> Reviews { get => reviews; set => reviews = value; }


        public static int InsertCountries(List<Country> countries)
        {
            DBservices dbs = new DBservices();

            foreach (Country country in countries)
            {
                country.RegionId = dbs.InsertRegion(country.Region);
                dbs.InsertCountry(country);

                foreach (Currency currency in country.Currencies)
                {
                    dbs.InsertCurrency(currency);
                    dbs.InsertCountryCurrency(country.Alpha3Code, currency.CurrencyCode);
                }

                foreach (Language language in country.Languages)
                {
                    dbs.InsertLanguage(language);
                    dbs.InsertCountryLanguage(country.Alpha3Code, language.LanguageCode);
                }
            }

            foreach (Country country in countries)
            {
                foreach (Country border in country.Borders)
                    dbs.InsertCountryBorder(country.Alpha3Code, border.Alpha3Code);
            }

            return countries.Count;
        }
    }
}
