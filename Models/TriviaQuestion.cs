using CountriesProject.DAL;

namespace CountriesProject.Models
{
    public class TriviaQuestion
    {
        int questionId;
        string questionText;
        string correctAnswer;
        string wrongAnswer1;
        string wrongAnswer2;
        string wrongAnswer3;
        List<string> answers;

        public TriviaQuestion() { }
        public TriviaQuestion(int questionId, string questionText, string correctAnswer, string wrongAnswer1, string wrongAnswer2, string wrongAnswer3)
        {
            QuestionId = questionId;
            QuestionText = questionText;
            CorrectAnswer = correctAnswer;
            WrongAnswer1 = wrongAnswer1;
            WrongAnswer2 = wrongAnswer2;
            WrongAnswer3 = wrongAnswer3;
        }

        public int QuestionId { get => questionId; set => questionId = value; }
        public string QuestionText { get => questionText; set => questionText = value; }
        public string CorrectAnswer { get => correctAnswer; set => correctAnswer = value; }
        public string WrongAnswer1 { get => wrongAnswer1; set => wrongAnswer1 = value; }
        public string WrongAnswer2 { get => wrongAnswer2; set => wrongAnswer2 = value; }
        public string WrongAnswer3 { get => wrongAnswer3; set => wrongAnswer3 = value; }
        public List<string> Answers { get => answers; set => answers = value; }

        public static List<TriviaQuestion> GetRandomTriviaQuestions()
        {
            DBservices dbs = new DBservices();
            List<TriviaQuestion> questions = dbs.GetRandomTriviaQuestionsFromDB();

            Random rnd = new Random();

            foreach (TriviaQuestion question in questions)
            {
                question.Answers = new List<string>
                {
                    question.CorrectAnswer,
                    question.WrongAnswer1,
                    question.WrongAnswer2,
                    question.WrongAnswer3
                };
                question.Answers = question.Answers.OrderBy(x => rnd.Next()).ToList();
            }
            return questions;
        }
    }
}
