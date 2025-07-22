namespace FootballManager.Core.DTOs
{
    public class CreateTeamDto
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int FoundedYear { get; set; }
        public string Coach { get; set; }
    }
}
