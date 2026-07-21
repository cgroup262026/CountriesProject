namespace CountriesProject.Models
{
    public class UserLanguage
    {
        private Language language;
        private int? proficiencyLevel;

        public UserLanguage(Language language, int? proficiencyLevel)
        {
            Language = language;
            ProficiencyLevel = proficiencyLevel;
        }

        public Language Language { get => language; set => language = value; }
        public int? ProficiencyLevel { get => proficiencyLevel; set => proficiencyLevel = value; }
    }
}
