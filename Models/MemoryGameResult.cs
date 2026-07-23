namespace CountriesProject.Models
{
    public class MemoryGameResult
    {
        int resultId;
        int userId;
        int? points;
        int? moves;
        int? matchedPairs;
        bool? isCompleted;
        DateTime playDate;

        public MemoryGameResult() { }
        public MemoryGameResult(int resultId, int userId, int? points, int? moves, int? matchedPairs, bool? isCompleted, DateTime playDate)
        {
            ResultId = resultId;
            UserId = userId;
            Points = points;
            Moves = moves;
            MatchedPairs = matchedPairs;
            IsCompleted = isCompleted;
            PlayDate = playDate;
        }

        public int ResultId { get => resultId; set => resultId = value; }
        public int UserId { get => userId; set => userId = value; }
        public int? Points { get => points; set => points = value; }
        public int? Moves { get => moves; set => moves = value; }
        public int? MatchedPairs { get => matchedPairs; set => matchedPairs = value; }
        public bool? IsCompleted { get => isCompleted; set => isCompleted = value; }
        public DateTime PlayDate { get => playDate; set => playDate = value; }
    }
}
