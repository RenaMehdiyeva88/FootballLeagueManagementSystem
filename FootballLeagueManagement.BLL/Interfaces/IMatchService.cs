using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.BLL.Interfaces
{
    public interface IMatchService
    {
        void AddMatch(Match match);
        List<Match> GetAll();
    }
}