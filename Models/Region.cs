namespace CountriesProject.Models
{
    public class Region
    {
        int regionID;
        string name;

        public Region(int regionID, string name)
        {
            this.regionID = regionID;
            this.name = name;
        }

        public int RegionID { get => regionID; set => regionID = value; }
        public string Name { get => name; set => name = value; }
    }
}
