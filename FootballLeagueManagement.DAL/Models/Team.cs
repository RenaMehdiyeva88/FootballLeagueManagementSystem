namespace FootballLeagueManagement.DAL.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Code { get; set; }
        public int? StadiumId { get; set; }
        public Stadium? Stadium { get; set; }
        public int Played { get; set; } = 0;
        public int Win { get; set; } = 0;
        public int Draw { get; set; } = 0;
        public int Lose { get; set; } = 0;
        public int GoalsFor { get; set; } = 0;
        public int GoalsAgainst { get; set; } = 0;
        public int Points { get; set; } = 0;
        public List<Player> Players { get; set; } = new();
    }
}