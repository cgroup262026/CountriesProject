namespace CountriesProject.Models
{
    public class TravelPreference
    {
        int preferenceID;
        string name;

        public TravelPreference(int preferenceID, string name)
        {
            this.preferenceID = preferenceID;
            this.name = name;
        }

        public int PreferenceID { get => preferenceID; set => preferenceID = value; }
        public string Name { get => name; set => name = value; }
    }
}
