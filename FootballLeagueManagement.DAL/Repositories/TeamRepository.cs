using FootballLeagueManagement.DAL.Context;
using FootballLeagueManagement.DAL.Interfaces;
using FootballLeagueManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballLeagueManagement.DAL.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly LeagueDbContext _context;
        public TeamRepository(LeagueDbContext context) => _context = context;

        public void Add(Team team) { _context.Teams.Add(team); _context.SaveChanges(); }
        public List<Team> GetAll() => _context.Teams.ToList();
        public List<Team> GetAllWithPlayers() => _context.Teams.Include(t => t.Players).ToList();
        public Team? GetById(int id) => _context.Teams.Include(t => t.Players).FirstOrDefault(t => t.Id == id);
        public void Update(Team team) { _context.Teams.Update(team); _context.SaveChanges(); }
        public void Delete(int id)
        {
            var t = GetById(id);
            if (t != null) { _context.Teams.Remove(t); _context.SaveChanges(); }
        }
    }
}