using FootballLeagueManagement.BLL.Interfaces;
using FootballLeagueManagement.DAL.Interfaces;
using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.BLL.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repo;
        public TeamService(ITeamRepository repo) => _repo = repo;

        public void AddTeam(Team team) => _repo.Add(team);
        public Team? GetTeamById(int id) => _repo.GetById(id);
        public void UpdateTeam(Team team) => _repo.Update(team);
        public void DeleteTeam(int id) => _repo.Delete(id);
        public List<Team> GetAllTeams() => _repo.GetAll();

        public List<Team> GetLeagueTable() => _repo.GetAllWithPlayers()
            .OrderByDescending(t => t.Points)
            .ThenByDescending(t => t.GoalsFor - t.GoalsAgainst)
            .ToList();

        public List<Team> GetTopScoringTeams() => _repo.GetAll()
            .OrderByDescending(t => t.GoalsFor)
            .ToList();

        public List<Team> GetMostGoalsConceded() => _repo.GetAll()
            .OrderByDescending(t => t.GoalsAgainst)
            .ToList();
    }
}