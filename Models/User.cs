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
        string email;
        string passwordHash;
        string fullName;
        DateTime birthDate;
        string gender;
        string imageUrl;
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
        public string Email { get => email; set => email = value; }
        public string PasswordHash { get => passwordHash; set => passwordHash = value; }
        public string FullName { get => fullName; set => fullName = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string Gender { get => gender; set => gender = value; }
        public string ImageUrl { get => imageUrl; set => imageUrl = value; }
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

            DBservices db = new DBservices();

            return db.InsertUserToDB(this);
        }

        public static User Login(string email, string password)
        {
            DBservices db = new DBservices();
            User user = db.GetUserByEmail(email);

            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;
            if (user.IsLocked) throw new Exception("User is locked");

            db.InsertUserLoginToDB(user.UserId);
            return user;
        }


    }
}