using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.Xml;

namespace CountriesProject.Models
{
    public class User
    {
        int userId;
        string email;
        string passwordHash;
        string fullName;
        private DateTime birthDate;
        string gender;
        string imageUrl;
        private List<string> hobbies;
        bool isAdmin;
        bool isLocked;
        int totalTriviaScore;
        int gameVictories;
        DateTime registrationDate;

        private List<UserLanguage> spokenLanguages = new List<UserLanguage>();
        private List<Region> favoriteRegions = new List<Region>();
        private List<Preference> preferences = new List<Preference>();
        private List<Country> visitedCountries = new List<Country>();
        private List<Country> wantToVisitCountries = new List<Country>();
        private List<Review> reviews = new List<Review>();
        private List<TriviaResult> triviaResults = new List<TriviaResult>();
        private List<MemoryGameResult> memoryGameResults = new List<MemoryGameResult>();

        public User() { }

        public User(int userId, string email, string passwordHash, string fullName, DateTime birthDate, string gender, string imageUrl, List<string> hobbies, bool isAdmin, bool isLocked, int totalTriviaScore, int gameVictories, DateTime registrationDate)
        {
            UserId = userId;
            Email = email;
            PasswordHash = passwordHash;
            FullName = fullName;
            BirthDate = birthDate;
            Gender = gender;
            ImageUrl = imageUrl;
            Hobbies = hobbies;
            IsAdmin = isAdmin;
            IsLocked = isLocked;
            TotalTriviaScore = totalTriviaScore;
            GameVictories = gameVictories;
            RegistrationDate = registrationDate;
        }

        public int UserId { get => userId; set => userId = value; }
        public string Email { get => email; set => email = value; }
        public string PasswordHash { get => passwordHash; set => passwordHash = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Gender { get => gender; set => gender = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
        public List<string> Hobbies { get => hobbies; set => hobbies = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public bool IsLocked { get => isLocked; set => isLocked = value; }
        public int TotalTriviaScore { get => totalTriviaScore; set => totalTriviaScore = value; }
        public int GameVictories { get => gameVictories; set => gameVictories = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }
        public List<UserLanguage> SpokenLanguages { get => spokenLanguages; set => spokenLanguages = value; }
        public List<Region> FavoriteRegions { get => favoriteRegions; set => favoriteRegions = value; }
        public List<Preference> Preferences { get => preferences; set => preferences = value; }
        public List<Country> VisitedCountries { get => visitedCountries; set => visitedCountries = value; }
        public List<Country> WantToVisitCountries { get => wantToVisitCountries; set => wantToVisitCountries = value; }
        public List<Review> Reviews { get => reviews; set => reviews = value; }
        public List<TriviaResult> TriviaResults { get => triviaResults; set => triviaResults = value; }
        public List<MemoryGameResult> MemoryGameResults { get => memoryGameResults; set => memoryGameResults = value; }
    }
}