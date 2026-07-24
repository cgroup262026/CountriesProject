using CountriesProject.DAL;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Text.Json;

namespace CountriesProject.Models
{
    public class Country
    {
        string name;
        string alpha3Code;
        string alpha2Code;
        string capital;
        string region;
        string subRegion;
        long population;
        double area;
        string flagUrl; 

        List<string> currencies  = new List<string>();
        List<string> languages  = new List<string>();
        List<string> borders  = new List<string>();

        public Country() { }

        public Country(string name, string alpha3Code, string alpha2Code, string capital, string region, string subRegion,
            long population, double area, string flagUrl, List<string> currencies, List<string> languages, List<string> borders)
        {
            Name = name;
            Alpha3Code = alpha3Code;
            Alpha2Code = alpha2Code;
            Capital = capital;
            Region = region;
            SubRegion = subRegion;
            Population = population;
            Area = area;
            FlagUrl = flagUrl;
            Currencies = currencies;
            Languages = languages;
            Borders = borders;
        }

        public string Name { get => name; set => name = value; }
        public string Alpha3Code { get => alpha3Code; set => alpha3Code = value; }
        public string Alpha2Code { get => alpha2Code; set => alpha2Code = value; }
        public string Capital { get => capital; set => capital = value; }
        public string Region { get => region; set => region = value; }
        public string SubRegion { get => subRegion; set => subRegion = value; }
        public long Population { get => population; set => population = value; }
        public double Area { get => area; set => area = value; }
        public string FlagUrl { get => flagUrl; set => flagUrl = value; }
        public List<string> Currencies { get => currencies; set => currencies = value; }
        public List<string> Languages { get => languages; set => languages = value; }
        public List<string> Borders { get => borders; set => borders = value; }

        public static async Task<int> ImportAllCountries()
        {
            using HttpClient client = new HttpClient();
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            string regionsJson = await client.GetStringAsync("https://countries.dev/regions");
            string currenciesJson = await client.GetStringAsync("https://countries.dev/currencies");
            string languagesJson = await client.GetStringAsync("https://countries.dev/languages");
            string countriesJson = await client.GetStringAsync("https://countries.dev/countries?fields=name,alpha2Code,alpha3Code,capital,subregion,region,population,area,borders,flags,currencies,languages");

            List<string> regions = JsonSerializer.Deserialize<List<string>>(regionsJson, options);
            List<ApiCurrency> currencies = JsonSerializer.Deserialize<List<ApiCurrency>>(currenciesJson, options);
            List<ApiLanguage> languages = JsonSerializer.Deserialize<List<ApiLanguage>>(languagesJson, options);
            List<ApiCountry> apiCountries = JsonSerializer.Deserialize<List<ApiCountry>>(countriesJson, options);

            DBservices dbs = new DBservices();

            // Insert basic data
            foreach (string region in regions) dbs.InsertRegionToDB(region);
            foreach (ApiCurrency currency in currencies) dbs.InsertCurrencyToDB(currency.Name);
            foreach (ApiLanguage language in languages) dbs.InsertLanguageToDB(language.Name);

            List<Country> countries = new List<Country>();

            // Create and insert all countries
            foreach (ApiCountry apiCountry in apiCountries)
            {
                Country country = new Country
                {
                    Name = apiCountry.Name,
                    Alpha3Code = apiCountry.Alpha3Code,
                    Alpha2Code = apiCountry.Alpha2Code,
                    Capital = apiCountry.Capital,
                    Region = apiCountry.Region,
                    SubRegion = apiCountry.Subregion,
                    Population = apiCountry.Population,
                    Area = apiCountry.Area,
                    FlagUrl = apiCountry.Flags == null ? "" : apiCountry.Flags.Svg,
                    Currencies = apiCountry.Currencies == null ? new List<string>() : apiCountry.Currencies.Select(c => c.Name).ToList(),
                    Languages = apiCountry.Languages == null ? new List<string>() : apiCountry.Languages.Select(l => l.Name).ToList(),
                    Borders = apiCountry.Borders == null ? new List<string>() : apiCountry.Borders
                };

                dbs.InsertCountryToDB(country);
                countries.Add(country);
            }

            // Make sure every currency and language used by a country exists
            foreach (Country country in countries)
            {
                foreach (string currency in country.Currencies) dbs.InsertCurrencyToDB(currency);
                foreach (string language in country.Languages) dbs.InsertLanguageToDB(language);
            }

            // Insert country-currency and country-language connections
            foreach (Country country in countries)
            {
                foreach (string currency in country.Currencies) dbs.InsertCountryCurrencyToDB(country.Alpha3Code, currency);
                foreach (string language in country.Languages) dbs.InsertCountryLanguageToDB(country.Alpha3Code, language);
            }

            // Insert borders last because all countries must already exist
            foreach (Country country in countries)
            {
                foreach (string border in country.Borders) dbs.InsertCountryBorderToDB(country.Alpha3Code, border);
            }

            return countries.Count;
        }

        public static List<Country> GetMemoryGameCountries()
        {
            DBservices dbs = new DBservices();
            return dbs.GetMemoryGameCountriesFromDB();
        }
    }
}
