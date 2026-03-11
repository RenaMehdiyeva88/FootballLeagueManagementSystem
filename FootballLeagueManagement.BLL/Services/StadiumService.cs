using FootballLeagueManagement.BLL.Interfaces;
using FootballLeagueManagement.DAL.Interfaces;
using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.BLL.Services
{
    public class StadiumService : IStadiumService
    {
        private readonly IStadiumRepository _repo;
        public StadiumService(IStadiumRepository repo) => _repo = repo;

        public void Add(Stadium stadium) => _repo.Add(stadium);
        public List<Stadium> GetAll() => _repo.GetAll();
        public void Delete(int id) => _repo.Delete(id);
    }
}