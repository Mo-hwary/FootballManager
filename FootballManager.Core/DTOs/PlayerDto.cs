namespace FootballManager.Core.DTOs
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Position { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
