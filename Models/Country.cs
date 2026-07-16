namespace CountriesProject.Models
{
    public class Country
    {
        string countryCode;
        string name;
        string capital;
        int population;
        float area;
        string flagImageURL;
        string currency;

        int regionID; //מפתח זר ליבשת

        List<Language> officialLanguage;
        List<Country> borderCountries;

        public Country(string countryCode, string name, string capital, int population, float area, string flagImageURL, 
            string currency, int regionID, List<Language> officialLanguage, List<Country> borderCountries)
        {
            CountryCode = countryCode;
            Name = name;
            Capital = capital;
            Population = population;
            Area = area;
            FlagImageURL = flagImageURL;
            Currency = currency;
            RegionID = regionID;
            OfficialLanguage = new List<Language>();
            BorderCountries = new List<Country>();
        }

        public string CountryCode { get => countryCode; set => countryCode = value; }
        public string Name { get => name; set => name = value; }
        public string Capital { get => capital; set => capital = value; }
        public int Population { get => population; set => population = value; }
        public float Area { get => area; set => area = value; }
        public string FlagImageURL { get => flagImageURL; set => flagImageURL = value; }
        public string Currency { get => currency; set => currency = value; }
        public int RegionID { get => regionID; set => regionID = value; }
        public List<Language> OfficialLanguage { get => officialLanguage; set => officialLanguage = value; }
        public List<Country> BorderCountries { get => borderCountries; set => borderCountries = value; }
    }
}
