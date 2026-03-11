using FootballLeagueManagement.BLL.Services;
using FootballLeagueManagement.DAL.Context;
using FootballLeagueManagement.DAL.Interfaces;
using FootballLeagueManagement.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FootballLeagueManagement.UI
{
    public class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var services = new ServiceCollection();

            services.AddDbContext<LeagueDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    sql => sql.EnableRetryOnFailure(5)));

            // Repositories
            services.AddScoped<IStadiumRepository, StadiumRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();

            // Services
            services.AddScoped<TeamService>();
            services.AddScoped<PlayerService>();
            services.AddScoped<StadiumService>();
            services.AddScoped<MatchService>();

            var provider = services.BuildServiceProvider();
            using var scope = provider.CreateScope();

            var teamService = scope.ServiceProvider.GetRequiredService<TeamService>();
            var playerService = scope.ServiceProvider.GetRequiredService<PlayerService>();
            var stadiumService = scope.ServiceProvider.GetRequiredService<StadiumService>();
            var matchService = scope.ServiceProvider.GetRequiredService<MatchService>();

            MenuOperation.Run(teamService, playerService, stadiumService, matchService);
        }
    }
}