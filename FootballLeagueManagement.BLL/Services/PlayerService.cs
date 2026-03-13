using FootballLeagueManagement.BLL.Interfaces;
using FootballLeagueManagement.DAL.Interfaces;
using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.BLL.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _repo;
        public PlayerService(IPlayerRepository repo) => _repo = repo;

        public void AddPlayer(Player player) => _repo.Add(player);
        public void DeletePlayer(int id) => _repo.Delete(id);
        public List<Player> GetAllPlayers() => _repo.GetAll();

        public List<Player> GetTopScorers() => _repo.GetAll()
            .OrderByDescending(p => p.Goals)
            .ToList();

        public void AddGoals(int teamId, int jerseyNumber, int goals)
        {
            var player = _repo.GetAll()
                .FirstOrDefault(p => p.TeamId == teamId && p.JerseyNumber == jerseyNumber);
            if (player != null)
            {
                player.Goals += goals;
                _repo.Update(player);
            }
        }
    }
}