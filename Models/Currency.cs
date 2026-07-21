namespace CountriesProject.Models
{
    public class Currency
    {
         string currencyCode;
         string currencyName;
         string symbol;

        public Currency(string currencyCode, string currencyName, string symbol)
        {
            CurrencyCode = currencyCode;
            CurrencyName = currencyName;
            Symbol = symbol;
        }

        public string CurrencyCode { get => currencyCode; set => currencyCode = value; }
        public string CurrencyName { get => currencyName; set => currencyName = value; }
        public string Symbol { get => symbol; set => symbol = value; }
    }
}
