using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.DAL.Interfaces
{
    public interface ITeamRepository
    {
        void Add(Team team);
        List<Team> GetAll();
        List<Team> GetAllWithPlayers();
        Team? GetById(int id);
        void Update(Team team);
        void Delete(int id);
    }
}