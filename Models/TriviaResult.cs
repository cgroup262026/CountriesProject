namespace CountriesProject.Models
{
    public class TriviaResult
    {
        int resultId;
        int userID;
        int? score;
        int? correctAnswers;
        int? totalQuestions;
        DateTime playDate;

        public TriviaResult() { }
        public TriviaResult(int resultId, int userID, int? score, int? correctAnswers, int? totalQuestions, DateTime playDate)
        {
            ResultId = resultId;
            UserID = userID;
            Score = score;
            CorrectAnswers = correctAnswers;
            TotalQuestions = totalQuestions;
            PlayDate = playDate;
        }

        public int ResultId { get => resultId; set => resultId = value; }
        public int UserID { get => userID; set => userID = value; }
        public int? Score { get => score; set => score = value; }
        public int? CorrectAnswers { get => correctAnswers; set => correctAnswers = value; }
        public int? TotalQuestions { get => totalQuestions; set => totalQuestions = value; }
        public DateTime PlayDate { get => playDate; set => playDate = value; }
    }
}
