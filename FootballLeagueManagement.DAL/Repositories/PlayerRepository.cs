using FootballLeagueManagement.DAL.Context;
using FootballLeagueManagement.DAL.Interfaces;
using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.DAL.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly LeagueDbContext _context;
        public PlayerRepository(LeagueDbContext context) => _context = context;

        public void Add(Player player) { _context.Players.Add(player); _context.SaveChanges(); }
        public List<Player> GetAll() => _context.Players.ToList();
        public Player? GetById(int id) => _context.Players.FirstOrDefault(p => p.Id == id);
        public void Update(Player player) { _context.Players.Update(player); _context.SaveChanges(); }
        public void Delete(int id)
        {
            var p = GetById(id);
            if (p != null) { _context.Players.Remove(p); _context.SaveChanges(); }
        }
    }
}