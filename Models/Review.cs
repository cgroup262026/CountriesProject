namespace CountriesProject.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public User User { get; set; } // מידע על המשתמש שכתב
        public string Alpha3Code { get; set; }
        public int Rating { get; set; }
        public string ReviewText { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
