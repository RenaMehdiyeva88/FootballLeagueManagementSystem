using FootballLeagueManagement.DAL.Context;
using FootballLeagueManagement.DAL.Interfaces;
using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.DAL.Repositories
{
    public class StadiumRepository : IStadiumRepository
    {
        private readonly LeagueDbContext _context;
        public StadiumRepository(LeagueDbContext context) => _context = context;

        public void Add(Stadium stadium) { _context.Stadiums.Add(stadium); _context.SaveChanges(); }
        public List<Stadium> GetAll() => _context.Stadiums.ToList();
        public Stadium? GetById(int id) => _context.Stadiums.FirstOrDefault(s => s.Id == id);
        public void Update(Stadium stadium) { _context.Stadiums.Update(stadium); _context.SaveChanges(); }
        public void Delete(int id)
        {
            var s = GetById(id);
            if (s != null) { _context.Stadiums.Remove(s); _context.SaveChanges(); }
        }
    }
}