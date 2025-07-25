namespace FootballManager.Core.DTOs
{
    public class CreateStatisticsDto
    {
        public int PlayerId { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int MinutesPlayed { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
    }
}
