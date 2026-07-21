namespace CountriesProject.Models
{
    public class TriviaResult
    {
        public int ResultId { get; set; }
        public int? Score { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int? CorrectAnswers { get; set; }
        public int? TotalQuestions { get; set; }
        public DateTime PlayDate { get; set; }
    }
}
