using FootballLeagueManagement.BLL.Services;
using FootballLeagueManagement.DAL.Models;

namespace FootballLeagueManagement.UI
{
    public class MenuOperation
    {
        public static void Run(
            TeamService teamService,
            PlayerService playerService,
            StadiumService stadiumService,
            MatchService matchService)
        {
            while (true)
            {
                PrintMenu();
                var choice = ReadInput("Enter your choice");

                switch (choice)
                {
                    case "1": AddStadium(stadiumService); break;
                    case "2": AddTeam(teamService); break;
                    case "3": AddPlayer(playerService); break;
                    case "4": AddMatch(matchService, teamService, playerService); break;
                    case "5": ViewTeamStatus(teamService); break;
                    case "6": ViewLeagueTable(teamService); break;
                    case "7": ViewTopScoringTeams(teamService); break;
                    case "8": ViewMostGoalsConceded(teamService); break;
                    case "9": ViewTopScorers(playerService, teamService); break;
                    case "0": Console.WriteLine("Goodbye!"); return;
                    default: Console.WriteLine("Invalid choice. Please try again."); break;
                }
            }
        }
        private static void PrintMenu()
        {
            Console.WriteLine("\n=== FOOTBALL LEAGUE MANAGER ===");
            Console.WriteLine("--- DATA ENTRY ---");
            Console.WriteLine("  1) Add Stadium");
            Console.WriteLine("  2) Add Team");
            Console.WriteLine("  3) Add Player");
            Console.WriteLine("  4) Add Match");
            Console.WriteLine("--- REPORTS ---");
            Console.WriteLine("  5) View Team Status");
            Console.WriteLine("  6) View League Table");
            Console.WriteLine("  7) View Top Scoring Teams");
            Console.WriteLine("  8) View Most Goals Conceded");
            Console.WriteLine("  9) View Top Scorers");
            Console.WriteLine("  0) Exit");
            Console.WriteLine("================================");
        }

        //HELPERS
        private static string ReadInput(string prompt)
        {
            Console.Write($"{prompt}: ");
            return Console.ReadLine() ?? "";
        }

        private static int ReadInt(string prompt)
        {
            Console.Write($"{prompt}: ");
            return int.Parse(Console.ReadLine() ?? "0");
        }

        private static void PrintTableHeader(string title, string header, int width)
        {
            Console.WriteLine($"\n===== {title} =====");
            Console.WriteLine(header);
            Console.WriteLine(new string('-', width));
        }

        //ADD STADIUM
        private static void AddStadium(StadiumService service)
        {
            Console.WriteLine("\n--- Add Stadium ---");
            var name = ReadInput("Stadium Name (max 100 chars)");
            var capacity = ReadInt("Capacity");

            service.Add(new Stadium { Name = name, Capacity = capacity });
            Console.WriteLine("Stadium added successfully.");
        }

        //ADD TEAM
        private static void AddTeam(TeamService service)
        {
            Console.WriteLine("\n--- Add Team ---");
            var name = ReadInput("Team Name");
            var code = ReadInt("Team Code (1-99)");
            var stadiumId = ReadInt("Stadium Id");

            service.AddTeam(new Team
            {
                Name = name,
                Code = code,
                StadiumId = stadiumId
            });
            Console.WriteLine("Team added successfully.");
        }

        //ADD PLAYER
        private static void AddPlayer(PlayerService service)
        {
            Console.WriteLine("\n--- Add Player ---");
            var name = ReadInput("Full Name");
            var jerseyNumber = ReadInt("Jersey Number");
            var teamId = ReadInt("Team Id");

            service.AddPlayer(new Player
            {
                FullName = name,
                JerseyNumber = jerseyNumber,
                TeamId = teamId,
                Goals = 0
            });
            Console.WriteLine("Player added successfully.");
        }

        //ADD MATCH
        private static void AddMatch(
            MatchService matchService,
            TeamService teamService,
            PlayerService playerService)
        {
            Console.WriteLine("\n--- Add Match ---");
            var week = ReadInt("Week Number");
            var homeId = ReadInt("Home Team Id");
            var awayId = ReadInt("Away Team Id");
            var hg = ReadInt("Home Goals");
            var ag = ReadInt("Away Goals");

            matchService.AddMatch(new Match
            {
                Week = week,
                HomeTeamId = homeId,
                AwayTeamId = awayId,
                HomeGoals = hg,
                AwayGoals = ag
            });

            UpdateTeamStats(teamService, homeId, awayId, hg, ag);
            UpdateGoalscorers(playerService);

            Console.WriteLine("Match added and statistics updated.");
        }

        private static void UpdateTeamStats(
            TeamService teamService,
            int homeId, int awayId,
            int hg, int ag)
        {
            var homeTeam = teamService.GetTeamById(homeId);
            var awayTeam = teamService.GetTeamById(awayId);
            if (homeTeam == null || awayTeam == null) return;

            homeTeam.Played++; awayTeam.Played++;
            homeTeam.GoalsFor += hg; homeTeam.GoalsAgainst += ag;
            awayTeam.GoalsFor += ag; awayTeam.GoalsAgainst += hg;

            if (hg > ag)
            {
                homeTeam.Win++; homeTeam.Points += 3;
                awayTeam.Lose++;
            }
            else if (hg < ag)
            {
                awayTeam.Win++; awayTeam.Points += 3;
                homeTeam.Lose++;
            }
            else
            {
                homeTeam.Draw++; homeTeam.Points++;
                awayTeam.Draw++; awayTeam.Points++;
            }

            teamService.UpdateTeam(homeTeam);
            teamService.UpdateTeam(awayTeam);
        }

        private static void UpdateGoalscorers(PlayerService playerService)
        {
            var answer = ReadInput("Enter goalscorers? (y/n)");
            if (answer.ToLower() != "y") return;

            var count = ReadInt("How many goalscorers");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nGoalscorer {i + 1}:");
                var teamId = ReadInt("  Team Id");
                var jersey = ReadInt("  Jersey Number");
                var goals = ReadInt("  Goals Scored");
                playerService.AddGoals(teamId, jersey, goals);
            }
        }

        //REPORT 1: TEAM STATUS
        private static void ViewTeamStatus(TeamService service)
        {
            var id = ReadInt("\nEnter Team Id");
            var team = service.GetTeamById(id);
            if (team == null) { Console.WriteLine("Team not found."); return; }

            PrintTableHeader("TEAM STATUS", FormatTeamHeader(), 57);
            Console.WriteLine(FormatTeamRow(team));

            Console.WriteLine("\n--- Players ---");
            Console.WriteLine($"{"Jersey No",10} {"Full Name",-22} {"Goals",6}");
            Console.WriteLine(new string('-', 42));

            if (!team.Players.Any())
            {
                Console.WriteLine("No players found.");
                return;
            }

            foreach (var p in team.Players.OrderByDescending(p => p.Goals))
                Console.WriteLine($"{p.JerseyNumber,10} {p.FullName,-22} {p.Goals,6}");
        }

        //REPORT 2: LEAGUE TABLE
        private static void ViewLeagueTable(TeamService service)
        {
            var teams = service.GetLeagueTable();
            PrintTableHeader("LEAGUE TABLE", FormatTeamHeader(), 57);

            if (!teams.Any()) { Console.WriteLine("No teams found."); return; }

            foreach (var t in teams)
                Console.WriteLine(FormatTeamRow(t));
        }

        //REPORT 3: TOP SCORING TEAMS
        private static void ViewTopScoringTeams(TeamService service)
        {
            var teams = service.GetTopScoringTeams();
            PrintTableHeader("TOP SCORING TEAMS", FormatGoalsHeader(), 42);

            if (!teams.Any()) { Console.WriteLine("No teams found."); return; }

            foreach (var t in teams)
                Console.WriteLine(FormatGoalsRow(t));
        }

        //REPORT 4: MOST GOALS CONCEDED
        private static void ViewMostGoalsConceded(TeamService service)
        {
            var teams = service.GetMostGoalsConceded();
            PrintTableHeader("MOST GOALS CONCEDED", FormatGoalsHeader(), 42);

            if (!teams.Any()) { Console.WriteLine("No teams found."); return; }

            foreach (var t in teams)
                Console.WriteLine(FormatGoalsRow(t));
        }

        //REPORT 5: TOP SCORERS
        private static void ViewTopScorers(PlayerService playerService, TeamService teamService)
        {
            var players = playerService.GetTopScorers();
            PrintTableHeader("TOP SCORERS", $"{"Team",-15} {"Jersey No",10} {"Full Name",-22} {"Goals",6}", 57);

            if (!players.Any()) { Console.WriteLine("No players found."); return; }

            foreach (var p in players)
            {
                var team = teamService.GetTeamById(p.TeamId);
                Console.WriteLine($"{team?.Name ?? "?",-15} {p.JerseyNumber,10} {p.FullName,-22} {p.Goals,6}");
            }
        }

        //FORMAT HELPERS
        private static string FormatTeamHeader() =>
            $"{"Team",-15} {"P",4} {"W",4} {"D",4} {"L",4} {"GF",5} {"GA",5} {"GD",5} {"Pts",5}";

        private static string FormatTeamRow(Team t) =>
            $"{t.Name,-15} {t.Played,4} {t.Win,4} {t.Draw,4} {t.Lose,4} {t.GoalsFor,5} {t.GoalsAgainst,5} {t.GoalsFor - t.GoalsAgainst,5} {t.Points,5}";

        private static string FormatGoalsHeader() =>
            $"{"Team",-15} {"GF",10} {"GA",10}";

        private static string FormatGoalsRow(Team t) =>
            $"{t.Name,-15} {t.GoalsFor,10} {t.GoalsAgainst,10}";
    }
}