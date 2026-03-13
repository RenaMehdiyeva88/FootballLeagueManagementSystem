using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.DAL.Interfaces
{
    public interface IPlayerRepository
    {
        void Add(Player player);
        List<Player> GetAll();
        Player? GetById(int id);
        void Update(Player player);
        void Delete(int id);
    }
}