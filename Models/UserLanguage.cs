namespace CountriesProject.Models
{
    public class UserLanguage
    {
        int userID;
        int languageID;

        int level;

        public UserLanguage(int userID, int languageID, int level)
        {
            this.UserID = userID;
            this.LanguageID = languageID;
            this.Level = level;
        }
        public int UserID { get => userID; set => userID = value; }
        public int LanguageID { get => languageID; set => languageID = value; }
        public int Level { get => level; set => level = value; }
    }
}
