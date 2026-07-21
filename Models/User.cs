using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.Xml;

namespace CountriesProject.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Hobbies { get; set; } = new List<string>(); public bool IsAdmin { get; set; }
        public bool IsLocked { get; set; }
        public int TotalTriviaScore { get; set; }
        public int GameVictories { get; set; }
        public DateTime RegistrationDate { get; set; }

        // רשימות מקושרות (Navigation Properties)
        public List<UserLanguage> SpokenLanguages { get; set; } = new List<UserLanguage>();
        public List<Region> FavoriteRegions { get; set; } = new List<Region>();
        public List<Preference> Preferences { get; set; } = new List<Preference>();
        public List<Country> VisitedCountries { get; set; } = new List<Country>();
        public List<Country> WantToVisitCountries { get; set; } = new List<Country>();
        public List<Review> Reviews { get; set; } = new List<Review>();
        public List<TriviaResult> TriviaResults { get; set; } = new List<TriviaResult>();
        public List<MemoryGameResult> MemoryGameResults { get; set; } = new List<MemoryGameResult>();
    }
}
