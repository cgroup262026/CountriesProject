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

        public static List<Country> GetAllCountries()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllCountriesFromDB();
        }

        public static Country GetCountryByCode(string alpha3Code)
        {
            DBservices dbs = new DBservices();

            Country country = dbs.GetCountryByCodeFromDB(alpha3Code);

            if (country != null)
            {
                country.Currencies = dbs.GetCountryCurrenciesFromDB(alpha3Code);
                country.Languages = dbs.GetCountryLanguagesFromDB(alpha3Code);
                country.Borders = dbs.GetCountryBordersFromDB(alpha3Code);
            }

            return country;
        }

        public int Insert()
        {
            DBservices dbs = new DBservices();
            List<Country> countries = dbs.GetAllCountriesFromDB();

            foreach (Country country in countries)
            {
                if (country.Alpha3Code == Alpha3Code || country.Alpha2Code == Alpha2Code) return 0;
            }

            dbs.InsertRegionToDB(Region);
            int numEffected = dbs.InsertCountryToDB(this);

            if (numEffected > 0)
            {
                foreach (string currency in Currencies)
                {
                    dbs.InsertCurrencyToDB(currency);
                    dbs.InsertCountryCurrencyToDB(Alpha3Code, currency);
                }

                foreach (string language in Languages)
                {
                    dbs.InsertLanguageToDB(language);
                    dbs.InsertCountryLanguageToDB(Alpha3Code, language);
                }

                foreach (string border in Borders)
                {
                    dbs.InsertCountryBorderToDB(Alpha3Code, border);
                }
            }

            return numEffected;
        }

        public int Update()
        {
            DBservices dbs = new DBservices();

            dbs.InsertRegionToDB(Region);
            int numEffected = dbs.UpdateCountryInDB(this);

            if (numEffected > 0)
            {
                dbs.ClearCountryDetailsFromDB(Alpha3Code);

                foreach (string currency in Currencies)
                {
                    dbs.InsertCurrencyToDB(currency);
                    dbs.InsertCountryCurrencyToDB(Alpha3Code, currency);
                }

                foreach (string language in Languages)
                {
                    dbs.InsertLanguageToDB(language);
                    dbs.InsertCountryLanguageToDB(Alpha3Code, language);
                }

                foreach (string border in Borders)
                {
                    dbs.InsertCountryBorderToDB(Alpha3Code, border);
                }
            }

            return numEffected;
        }

        public static int Delete(string alpha3Code)
        {
            DBservices dbs = new DBservices();
            return dbs.DeleteCountryFromDB(alpha3Code);
        }

        public static List<Country> SearchCountries(string? name, string? region, string? language, string? currency, long? minPopulation, long? maxPopulation, double? minArea, double? maxArea, string sortBy, string sortDirection)
        {
            DBservices dbs = new DBservices();
            return dbs.SearchCountriesFromDB(name, region, language, currency, minPopulation, maxPopulation, minArea, maxArea, sortBy, sortDirection);
        }

        public static List<string> GetAllCurrencies()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllCurrenciesFromDB();
        }

        public static List<Country> GetMemoryGameCountries()
        {
            DBservices dbs = new DBservices();
            return dbs.GetMemoryGameCountriesFromDB();
        }

        
    }
}
