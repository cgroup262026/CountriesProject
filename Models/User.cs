namespace CountriesProject.Models
{
    public class User
    {
        int userID;
        string email;
        string password;
        string fullName;
        DateTime birthDate;
        string imageURL;
        bool isAdmin;
        int totalTriviaScore;
        bool isLocked;

        private List<TravelPreference> preferences;
        private List<UserLanguage> spokenLanguages;
        private List<Country> visitedCountries;
        private List<Country> wantToVisitCountries;

        public User(int userID, string email, string password, string fullName, DateTime birthDate, string imageURL, bool isAdmin, int totalTriviaScore, bool isLocked,
                        List<TravelPreference> preferences, List<UserLanguage> spokenLanguages, List<Country> visitedCountries, List<Country> wantToVisitCountries)
        {
            this.userID = userID;
            this.email = email;
            this.password = password;
            this.fullName = fullName;
            this.birthDate = birthDate;
            this.imageURL = imageURL;
            this.isAdmin = isAdmin;
            this.totalTriviaScore = totalTriviaScore;
            this.isLocked = isLocked;
            Preferences = new List<TravelPreference>();
            SpokenLanguages = new List<UserLanguage>();
            VisitedCountries = new List<Country>();
            WantToVisitCountries = new List<Country>();
        }

        public int UserID { get => userID; set => userID = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string ImageURL { get => imageURL; set => imageURL = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public int TotalTriviaScore { get => totalTriviaScore; set => totalTriviaScore = value; }
        public bool IsLocked { get => isLocked; set => isLocked = value; }
        public List<TravelPreference> Preferences { get => preferences; set => preferences = value; }
        public List<UserLanguage> SpokenLanguages { get => spokenLanguages; set => spokenLanguages = value; }
        public List<Country> VisitedCountries { get => visitedCountries; set => visitedCountries = value; }
        public List<Country> WantToVisitCountries { get => wantToVisitCountries; set => wantToVisitCountries = value; }
    }
}
