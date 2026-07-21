namespace CountriesProject.Models
{
    public static class HobbyDictionary
    {
        // הרשימה הסגורה של כל התחביבים האפשריים במערכת (המאגר)
        public static readonly List<string> AllHobbies = new List<string>
    {
        "קולינריה ואוכל",
        "טבע ונופים",
        "ספורט אקסטרים",
        "היסטוריה ותרבות",
        "חיי לילה",
        "קניות (שופינג)",
        "מוזיאונים ואמנות",
        "בטן-גב וחופים",
        // ... ניתן להוסיף כאן את כל ה-20 ...
    };

        // מתודה לבדיקה אם התחביב שהמשתמש שלח באמת חוקי וקיים במאגר שלנו
        public static bool IsValidHobby(string hobby)
        {
            return AllHobbies.Contains(hobby);
        }
    }
}
