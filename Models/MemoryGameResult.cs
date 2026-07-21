namespace CountriesProject.Models
{
    public class MemoryGameResult
    {
        public int ResultId { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        public int? Points { get; set; }
        public int? Moves { get; set; }
        public int? MatchedPairs { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime PlayDate { get; set; }
    }
}
