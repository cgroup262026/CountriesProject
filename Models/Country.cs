using CountriesProject.DAL;
using Microsoft.AspNetCore.Mvc.ViewEngines;

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
    }
}
