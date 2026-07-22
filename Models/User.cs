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
        DateTime birthDate;
        string gender;
        string imageUrl;
        List<string> hobbies;
        bool isAdmin;
        bool isLocked;
        int totalTriviaScore;
        int gameVictories;
        DateTime registrationDate;

        private List<string> spokenLanguages = new List<string>();
        private List<string> favoriteRegions = new List<string>();
        private List<string> travelPreferences = new List<string>();
        private List<Country> visitedCountries = new List<Country>();
        private List<Country> wantToVisitCountries = new List<Country>();
        private List<Review> reviews = new List<Review>();
        private List<TriviaResult> triviaResults = new List<TriviaResult>();
        private List<MemoryGameResult> memoryGameResults = new List<MemoryGameResult>();

        public User() { }


    }
}