using CountriesProject.DAL;
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

        public static List<string> GetAllLanguages()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllLanguagesFromDB();
        }

        public static void UpdateUserLanguages(int userId, List<UserLanguage> languages)
        {
            DBservices dbs = new DBservices();
            dbs.ClearUserLanguagesFromDB(userId);

            foreach (UserLanguage language in languages)
            {
                dbs.AddUserLanguageToDB(userId, language);
            }
        }
    }
}
