namespace CountriesProject.Models
{
    public class Region
    {
        private int regionId;
        private string regionName;

        public Region(int regionId, string regionName)
        {
            RegionId = regionId;
            RegionName = regionName;
        }

        public int RegionId { get => regionId; set => regionId = value; }
        public string RegionName { get => regionName; set => regionName = value; }
    }
}
