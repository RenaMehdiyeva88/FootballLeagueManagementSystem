using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.BLL.Interfaces
{
    public interface IPlayerService
    {
        void AddPlayer(Player player);
        void DeletePlayer(int id);
        List<Player> GetAllPlayers();
        List<Player> GetTopScorers();
        void AddGoals(int teamId, int jerseyNumber, int goals);
    }
}