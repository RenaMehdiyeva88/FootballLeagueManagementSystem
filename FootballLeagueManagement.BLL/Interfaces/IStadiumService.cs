using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.BLL.Interfaces
{
    public interface IStadiumService
    {
        void Add(Stadium stadium);
        List<Stadium> GetAll();
        void Delete(int id);
    }
}