using System.ComponentModel.DataAnnotations;

namespace CountriesProject.Models
{
    public class UserLanguage
    {
        string languageName;
        int proficiencyLevel;

        public UserLanguage() { }
        public UserLanguage(string languageName, int proficiencyLevel)
        {
            LanguageName = languageName;
            ProficiencyLevel = proficiencyLevel;
        }

        public string LanguageName { get => languageName; set => languageName = value; }
        public int ProficiencyLevel { get => proficiencyLevel; set => proficiencyLevel = value; }
    }
}
