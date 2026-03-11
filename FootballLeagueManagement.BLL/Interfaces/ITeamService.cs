using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.BLL.Interfaces
{
    public interface ITeamService
    {
        void AddTeam(Team team);
        Team? GetTeamById(int id);
        void UpdateTeam(Team team);
        void DeleteTeam(int id);
        List<Team> GetAllTeams();
        List<Team> GetLeagueTable();
        List<Team> GetTopScoringTeams();
        List<Team> GetMostGoalsConceded();
    }
}