namespace CountriesProject.Models
{
    public class Review
    {
        private int reviewId;
        private User user;
        private string alpha3Code;
        private int rating;
        private string reviewText;
        private DateTime publishDate;

        public Review(int reviewId, User user, string alpha3Code, int rating, string reviewText, DateTime publishDate)
        {
            ReviewId = reviewId;
            User = user;
            Alpha3Code = alpha3Code;
            Rating = rating;
            ReviewText = reviewText;
            PublishDate = publishDate;
        }

        public int ReviewId { get => reviewId; set => reviewId = value; }
        public User User { get => user; set => user = value; }
        public string Alpha3Code { get => alpha3Code; set => alpha3Code = value; }
        public int Rating { get => rating; set => rating = value; }
        public string ReviewText { get => reviewText; set => reviewText = value; }
        public DateTime PublishDate { get => publishDate; set => publishDate = value; }
    }
}
