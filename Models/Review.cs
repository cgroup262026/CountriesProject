namespace CountriesProject.Models
{
    public class Review
    {
        int reviewId;
        int userID;
        string alpha3Code;
        int rating;
        string reviewText;
        DateTime publishDate;

        public Review() { }

        public Review(int reviewId, int userID, string alpha3Code, int rating, string reviewText, DateTime publishDate)
        {
            ReviewId = reviewId;
            UserID = userID;
            Alpha3Code = alpha3Code;
            Rating = rating;
            ReviewText = reviewText;
            PublishDate = publishDate;
        }

        public int ReviewId { get => reviewId; set => reviewId = value; }
        public int UserID { get => userID; set => userID = value; }
        public string Alpha3Code { get => alpha3Code; set => alpha3Code = value; }
        public int Rating { get => rating; set => rating = value; }
        public string ReviewText { get => reviewText; set => reviewText = value; }
        public DateTime PublishDate { get => publishDate; set => publishDate = value; }
    }
}
