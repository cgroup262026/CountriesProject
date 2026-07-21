namespace CountriesProject.Models
{
    public class TriviaResult
    {
        private int resultId;
        private int? score;
        private int userId;
        private User user;
        private int? correctAnswers;
        private int? totalQuestions;
        private DateTime playDate;

        public TriviaResult(int resultId, int? score, int userId, User user, int? correctAnswers, int? totalQuestions, DateTime playDate)
        {
            ResultId = resultId;
            Score = score;
            UserId = userId;
            User = user;
            CorrectAnswers = correctAnswers;
            TotalQuestions = totalQuestions;
            PlayDate = playDate;
        }

        public int ResultId { get => resultId; set => resultId = value; }
        public int? Score { get => score; set => score = value; }
        public int UserId { get => userId; set => userId = value; }
        public User User { get => user; set => user = value; }
        public int? CorrectAnswers { get => correctAnswers; set => correctAnswers = value; }
        public int? TotalQuestions { get => totalQuestions; set => totalQuestions = value; }
        public DateTime PlayDate { get => playDate; set => playDate = value; }
    }
}
