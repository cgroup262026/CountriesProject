namespace CountriesProject.Models
{
    public class Language
    {
        string languageCode;
        string languageName;

        public Language(string languageCode, string languageName)
        {
            LanguageCode = languageCode;
            LanguageName = languageName;
        }

        public string LanguageCode { get => languageCode; set => languageCode = value; }
        public string LanguageName { get => languageName; set => languageName = value; }
    }
}
