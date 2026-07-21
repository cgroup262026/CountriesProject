using System.Text.Json.Serialization;


namespace CountriesProject.DTOs
{
    public class CountryDTO
    {
        [JsonPropertyName("name")]
        public string? countryName { get; set; }

        [JsonPropertyName("alpha2Code")]
        public string? alpha2Code { get; set; }

        [JsonPropertyName("alpha3Code")]
        public string? alpha3Code { get; set; }

        [JsonPropertyName("capital")]
        public string? capital { get; set; }

        [JsonPropertyName("region")]
        public string? region { get; set; }

        [JsonPropertyName("subregion")]
        public string? subregion { get; set; }

        [JsonPropertyName("population")]
        public long population { get; set; }

        [JsonPropertyName("area")]
        public double area { get; set; }

        [JsonPropertyName("borders")]
        public List<string>? borders { get; set; }

        [JsonPropertyName("flags")]
        public FlagsDTO? flags { get; set; }

        [JsonPropertyName("currencies")]
        public List<CurrencyDTO>? currencies { get; set; }

        [JsonPropertyName("languages")]
        public List<LanguageDTO>? languages { get; set; }
    }



    public class CurrencyDTO
    {
        [JsonPropertyName("code")]
        public string? currencyCode { get; set; }

        [JsonPropertyName("name")]
        public string? currencyName { get; set; }

        [JsonPropertyName("symbol")]
        public string? symbol { get; set; }
    }


    public class LanguageDTO
    {

        [JsonPropertyName("iso639_2")]
        public string? languageCode { get; set; }

        [JsonPropertyName("name")]
        public string? languageName { get; set; }

    }

    public class FlagsDTO
    {
        [JsonPropertyName("svg")]
        public string? Svg { get; set; }
    }
}