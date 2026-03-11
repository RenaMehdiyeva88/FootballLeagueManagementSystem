using FootballLeagueManagement.DAL.Context;
using FootballLeagueManagement.DAL.Interfaces;
using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.DAL.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly LeagueDbContext _context;
        public MatchRepository(LeagueDbContext context) => _context = context;

        public void Add(Match match) { _context.Matches.Add(match); _context.SaveChanges(); }
        public List<Match> GetAll() => _context.Matches.ToList();
        public Match? GetById(int id) => _context.Matches.FirstOrDefault(m => m.Id == id);
        public void Update(Match match) { _context.Matches.Update(match); _context.SaveChanges(); }
        public void Delete(int id)
        {
            var m = GetById(id);
            if (m != null) { _context.Matches.Remove(m); _context.SaveChanges(); }
        }
    }
}