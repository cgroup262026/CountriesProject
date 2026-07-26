using CountriesProject.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.Xml;

namespace CountriesProject.Models
{
    public class User
    {
        int userId;
        string? email;
        string? passwordHash;
        string? fullName;
        DateTime birthDate;
        string? gender;
        string? imageUrl;
        bool isAdmin;
        bool isLocked;
        DateTime registrationDate;

        List<string> hobbies = new List<string>();
        List<UserLanguage> spokenLanguages = new List<UserLanguage>();
        List<string> favoriteRegions = new List<string>();
        List<string> travelPreferences = new List<string>();


        public User() { }

        public User(string email, string passwordHash, string fullName, DateTime birthDate, string gender)
        {
            Email = email;
            PasswordHash = passwordHash;
            FullName = fullName;
            BirthDate = birthDate;
            Gender = gender;
        }

        public int UserId { get => userId; set => userId = value; }
        public string? Email { get => email; set => email = value; }
        public string? PasswordHash { get => passwordHash; set => passwordHash = value; }
        public string? FullName { get => fullName; set => fullName = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string? Gender { get => gender; set => gender = value; }
        public string? ImageUrl { get => imageUrl; set => imageUrl = value; }
        public bool IsAdmin { get => isAdmin; set => isAdmin = value; }
        public bool IsLocked { get => isLocked; set => isLocked = value; }
        public List<string> Hobbies { get => hobbies; set => hobbies = value; }
        public List<UserLanguage> SpokenLanguages { get => spokenLanguages; set => spokenLanguages = value; }
        public List<string> FavoriteRegions { get => favoriteRegions; set => favoriteRegions = value; }
        public List<string> TravelPreferences { get => travelPreferences; set => travelPreferences = value; }
        public DateTime RegistrationDate { get => registrationDate; set => registrationDate = value; }

        public int Register()
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(PasswordHash);

            DBservices dbs = new DBservices();

            return dbs.InsertUserToDB(this);
        }

        public static User Login(string email, string password)
        {
            DBservices dbs = new DBservices();
            User user = dbs.GetUserByEmail(email);

            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;
            if (user.IsLocked) throw new UnauthorizedAccessException("User is locked.");

            dbs.InsertUserLoginToDB(user.UserId);
            return user;
        }

        public static List<User> GetAllUsers()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllUsersFromDB();
        }

        public static User GetUserById(int userId)
        {
            DBservices dbs = new DBservices();
            User user = dbs.GetUserByIdFromDB(userId);

            if (user != null)
            {
                user.Hobbies = dbs.GetUserHobbiesFromDB(userId);
                user.SpokenLanguages = dbs.GetUserLanguagesFromDB(userId);
                user.FavoriteRegions = dbs.GetUserRegionsFromDB(userId);
                user.TravelPreferences = dbs.GetUserTravelPreferencesFromDB(userId);
            }
            return user;
        }

        public int UpdateUser()
        {
            DBservices dbs = new DBservices();
            return dbs.UpdateUserInDB(this);
        }

        public static User? UpdateProfile (int userId, User updatedUser)
        {
            User? existingUser = GetUserById(userId);

            if (existingUser == null)
                return null;

            if (!string.IsNullOrWhiteSpace(updatedUser.FullName))
                existingUser.FullName = updatedUser.FullName.Trim();
            
            if (updatedUser.BirthDate != default)
            {
                DateTime birthDate = updatedUser.BirthDate.Date;
                DateTime today = DateTime.Today;
                if (birthDate > today)
                    throw new ArgumentException("Birth date cannot be in the future.");

                if (birthDate > today.AddYears(-13))
                    throw new ArgumentException("The user must be at least 13 years old.");
                existingUser.BirthDate = birthDate;
            }

            if (!string.IsNullOrWhiteSpace(updatedUser.Gender))
                existingUser.Gender = updatedUser.Gender.Trim();

            if (updatedUser.ImageUrl != null)
                existingUser.ImageUrl = updatedUser.ImageUrl.Trim();

            int affectedRows = existingUser.UpdateUser();

            if (affectedRows <= 0)
                throw new InvalidOperationException("The profile could not be updated.");

            return existingUser;
        }

        public static int DeleteOrLockUser(int id)
        {
            DBservices dbs = new DBservices();
            return dbs.DeleteOrLockUserInDB(id);
        }

        public static int UpdateLockStatus(int id, bool isLocked)
        {
            DBservices db = new DBservices();
            return db.UpdateUserLockStatusInDB(id, isLocked);
        }

        public static List<string> GetAllHobbies()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllHobbiesFromDB();
        }

        public static List<string> GetUserHobbies(int userId)
        {
            DBservices dbs = new DBservices();
            return dbs.GetUserHobbiesFromDB(userId);
        }

        public static void UpdateUserHobbies(int userId, List<string> hobbies)
        {
            DBservices db = new DBservices();
            db.UpdateUserHobbiesInDB(userId, hobbies);
        }

        public static int GetUserTotalScore(int userId)
        {
            DBservices dbs = new DBservices();
            return dbs.GetUserTotalScoreFromDB(userId);
        }

        public static int AddSavedCountry(int userId, string alpha3Code, string listType)
        {
            DBservices dbs = new DBservices();
            return dbs.AddUserSavedCountryToDB(userId, alpha3Code, listType);
        }

        public static int DeleteSavedCountry(int userId, string alpha3Code)
        {
            DBservices dbs = new DBservices();

            Review? countryReview = Review.GetReviewsByUser(userId).FirstOrDefault(review =>
                string.Equals(review.Alpha3Code, alpha3Code, StringComparison.OrdinalIgnoreCase));

            int affectedRows = dbs.DeleteUserSavedCountryFromDB(userId, alpha3Code);

            if (affectedRows != 0 && countryReview != null)
            {
                Review.Delete(countryReview.ReviewId, userId);
            }

            return affectedRows;
        }

        public static List<Country> GetSavedCountries(int userId, string listType)
        {
            DBservices dbs = new DBservices();
            return dbs.GetUserSavedCountriesFromDB(userId, listType);
        }

        public static List<string> GetAllRegions()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllRegionsFromDB();
        }

        public static void UpdateUserRegions(int userId, List<string> regions)
        {
            DBservices dbs = new DBservices();
            dbs.ClearUserRegionsFromDB(userId);

            foreach (string region in regions)
            {
                dbs.AddUserRegionToDB(userId, region);
            }
        }

        public static List<string> GetAllTravelPreferences()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllTravelPreferencesFromDB();
        }

        public static void UpdateUserTravelPreferences(int userId, List<string> preferences)
        {
            DBservices dbs = new DBservices();
            dbs.ClearUserTravelPreferencesFromDB(userId);

            foreach (string preference in preferences)
            {
                dbs.AddUserTravelPreferenceToDB(userId, preference);
            }
        }
    }
}