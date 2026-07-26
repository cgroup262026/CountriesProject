using CountriesProject.DAL;

namespace CountriesProject.Models
{
    public class Review
    {
        int reviewId;
        int userID;
        string? alpha3Code;
        int rating;
        string reviewText;
        DateTime publishDate;

        string? fullName;
        string? imageUrl;
        string? countryName;

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
        public string? Alpha3Code { get => alpha3Code; set => alpha3Code = value; }
        public int Rating { get => rating; set => rating = value; }
        public string ReviewText { get => reviewText; set => reviewText = value; }
        public DateTime PublishDate { get => publishDate; set => publishDate = value; }
        public string? FullName { get => fullName; set => fullName = value; }
        public string? ImageUrl { get => imageUrl; set => imageUrl = value; }
        public string? CountryName { get => countryName; set => countryName = value; }

        public static List<Review> GetAllReviews()
        {
            DBservices dbs = new DBservices();
            return dbs.GetAllReviewsFromDB();
        }

        public static List<Review> GetReviewsByCountry(string alpha3Code)
        {
            DBservices dbs = new DBservices();
            return dbs.GetReviewsByCountryFromDB(alpha3Code);
        }

        public static List<Review> GetReviewsByUser(int userId)
        {
            DBservices dbs = new DBservices();
            return dbs.GetReviewsByUserFromDB(userId);
        }

        public int Insert()
        {
            if (UserID <= 0)
            {
                throw new ArgumentException(
                    "A valid user is required."
                );
            }

            if (string.IsNullOrWhiteSpace(Alpha3Code))
            {
                throw new ArgumentException(
                    "Country code is required."
                );
            }

            ValidateReviewContent();

            DBservices dbs = new DBservices();
            return dbs.InsertReviewToDB(this);
        }

        public int Update()
        {
            if (ReviewId <= 0 || UserID <= 0)
            {
                throw new ArgumentException(
                    "Invalid review or user."
                );
            }

            ValidateReviewContent();

            DBservices dbs = new DBservices();
            return dbs.UpdateReviewInDB(this);
        }

        private void ValidateReviewContent()
        {
            if (Rating < 1 || Rating > 5)
            {
                throw new ArgumentException(
                    "Rating must be between 1 and 5."
                );
            }

            if (string.IsNullOrWhiteSpace(ReviewText))
            {
                throw new ArgumentException(
                    "Review text is required."
                );
            }
        }

        public static int Delete(int reviewId, int userId)
        {
            DBservices dbs = new DBservices();
            return dbs.DeleteReviewFromDB(reviewId, userId);
        }
    }
}
