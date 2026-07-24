using CountriesProject.DAL;

namespace CountriesProject.Models
{
    public class AdminStatistics
    {
        int dailyLogins;
        int totalCountries;
        int savedCountries;
        int totalReviews;

        public AdminStatistics() { }
        public AdminStatistics(int dailyLogins, int totalCountries, int savedCountries, int totalReviews)
        {
            DailyLogins = dailyLogins;
            TotalCountries = totalCountries;
            SavedCountries = savedCountries;
            TotalReviews = totalReviews;
        }

        public int DailyLogins { get => dailyLogins; set => dailyLogins = value; }
        public int TotalCountries { get => totalCountries; set => totalCountries = value; }
        public int SavedCountries { get => savedCountries; set => savedCountries = value; }
        public int TotalReviews { get => totalReviews; set => totalReviews = value; }

        public static AdminStatistics GetStatistics()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAdminStatisticsFromDB();
        }
    }
}
