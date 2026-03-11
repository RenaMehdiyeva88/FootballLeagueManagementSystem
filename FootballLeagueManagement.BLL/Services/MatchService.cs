using FootballLeagueManagement.BLL.Interfaces;
using FootballLeagueManagement.DAL.Interfaces;
using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.BLL.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _repo;
        public MatchService(IMatchRepository repo) => _repo = repo;

        public void AddMatch(Match match) => _repo.Add(match);
        public List<Match> GetAll() => _repo.GetAll();
    }
}