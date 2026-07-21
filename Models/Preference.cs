namespace CountriesProject.Models
{
    public class Preference
    {
        int preferenceId;
        string preferenceName;

        public Preference(int preferenceId, string preferenceName)
        {
            PreferenceId = preferenceId;
            PreferenceName = preferenceName;
        }

        public int PreferenceId { get => preferenceId; set => preferenceId = value; }
        public string PreferenceName { get => preferenceName; set => preferenceName = value; }
    }
}
