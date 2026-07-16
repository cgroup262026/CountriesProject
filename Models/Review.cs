namespace CountriesProject.Models
{
    public class Review
    {
        int reviewID;
        string reviewText;
        int rating;
        DateTime publishDate;

        int userID;
        string countryCode;

        public Review(int reviewID, string reviewText, int rating, DateTime publishDate, int userID, string countryCode)
        {
            this.reviewID = reviewID;
            this.reviewText = reviewText;
            this.rating = rating;
            this.publishDate = publishDate;
            this.userID = userID;
            this.countryCode = countryCode;
        }

        public int ReviewID { get => reviewID; set => reviewID = value; }
        public string ReviewText { get => reviewText; set => reviewText = value; }
        public int Rating { get => rating; set => rating = value; }
        public DateTime PublishDate { get => publishDate; set => publishDate = value; }
        public int UserID { get => userID; set => userID = value; }
        public string CountryCode { get => countryCode; set => countryCode = value; }
    }
}
