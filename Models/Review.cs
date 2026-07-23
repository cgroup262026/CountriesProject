namespace CountriesProject.Models
{
    public class Review
    {
        int reviewId;
        int userID;
        string countryName;
        int rating;
        string reviewText;
        DateTime publishDate;

        public Review() { }

        public Review(int reviewId, int userID, string countryName, int rating, string reviewText, DateTime publishDate)
        {
            ReviewId = reviewId;
            UserID = userID;
            CountryName = countryName;
            Rating = rating;
            ReviewText = reviewText;
            PublishDate = publishDate;
        }

        public int ReviewId { get => reviewId; set => reviewId = value; }
        public int UserID { get => userID; set => userID = value; }
        public string CountryName { get => countryName; set => countryName = value; }
        public int Rating { get => rating; set => rating = value; }
        public string ReviewText { get => reviewText; set => reviewText = value; }
        public DateTime PublishDate { get => publishDate; set => publishDate = value; }
    }
}
