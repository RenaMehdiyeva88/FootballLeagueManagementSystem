using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.DAL.Interfaces
{
    public interface IMatchRepository
    {
        void Add(Match match);
        List<Match> GetAll();
        Match? GetById(int id);
        void Update(Match match);
        void Delete(int id);
    }
}