namespace FootballLeagueManagement.DAL.Models
{
    public class Stadium
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
    }
}