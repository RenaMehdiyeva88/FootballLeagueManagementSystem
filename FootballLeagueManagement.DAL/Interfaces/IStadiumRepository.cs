using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.DAL.Interfaces
{
    public interface IStadiumRepository
    {
        void Add(Stadium stadium);
        List<Stadium> GetAll();
        Stadium? GetById(int id);
        void Update(Stadium stadium);
        void Delete(int id);
    }
}