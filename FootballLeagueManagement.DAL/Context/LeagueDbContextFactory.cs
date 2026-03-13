using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FootballLeagueManagement.DAL.Context
{
    public class LeagueDbContextFactory : IDesignTimeDbContextFactory<LeagueDbContext>
    {
        public LeagueDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LeagueDbContext>();
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-AT927PS;Database=FLMDB;Trusted_Connection=True;TrustServerCertificate=True;");
            return new LeagueDbContext(optionsBuilder.Options);
        }
    }
}