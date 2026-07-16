namespace CountriesProject.Models
{
    public class Language
    {
        int languageID;
        string name;

        public Language(int languageID, string name)
        {
            LanguageID = languageID;
            Name = name;
        }

        public int LanguageID { get => languageID; set => languageID = value; }
        public string Name { get => name; set => name = value; }
    }
}
