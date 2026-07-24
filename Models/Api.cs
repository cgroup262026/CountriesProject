namespace CountriesProject.Models
{
    public class ApiCountry
    {
        public string Name { get; set; }
        public string Alpha3Code { get; set; }
        public string Alpha2Code { get; set; }
        public string Capital { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }
        public long Population { get; set; }
        public double Area { get; set; }
        public ApiFlags Flags { get; set; }
        public List<ApiCurrency> Currencies { get; set; } = new List<ApiCurrency>();
        public List<ApiLanguage> Languages { get; set; } = new List<ApiLanguage>();
        public List<string> Borders { get; set; } = new List<string>();
    }

    public class ApiCurrency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
    }

    public class ApiLanguage
    {
        public string Iso639_1 { get; set; }
        public string Iso639_2 { get; set; }
        public string Name { get; set; }
        public string NativeName { get; set; }
    }

    public class ApiFlags
    {
        public string Svg { get; set; }
        public string Png { get; set; }
    }
}
