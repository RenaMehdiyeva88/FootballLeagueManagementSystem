# FootballLeagueManagementSystem

A C# (.NET) console application for managing a football (soccer) league, backed by Entity Framework Core and SQL Server. It supports managing stadiums, teams, players, and matches, plus a set of built-in league reports (standings, top scorers, goals conceded, and more).

## Features

**Data Entry**
- Add a stadium (name, capacity)
- Add a team (name, code, linked stadium)
- Add a player (linked to a team)
- Add a match (linked teams, stadium, score)

**Reports**
- View a team's current status
- View the full league table (standings)
- View the top scoring teams
- View teams with the most goals conceded
- View the top scoring players

## Technologies

- C#
- .NET
- Console application
- Entity Framework Core (with migrations)
- SQL Server
- Dependency Injection (Microsoft.Extensions.DependencyInjection)
- Layered architecture (DAL / BLL / UI) with repository and service patterns

## Architecture

The solution is split into three projects:

- **FootballLeagueManagement.DAL** — Data Access Layer: `LeagueDbContext`, entities (Team, Player, Stadium, Match), repositories, and EF Core migrations.
- **FootballLeagueManagement.BLL** — Business Logic Layer: `TeamService`, `PlayerService`, `StadiumService`, `MatchService`.
- **FootballLeagueManagement.UI** — Presentation Layer: `Program.cs` (DI and configuration setup) and `MenuOperation.cs` (console menu logic).

The repository also includes standalone SQL table scripts (Teams, Players, Stadiums, Matches, and EF migrations history) for reference.

## Requirements

- .NET SDK installed (https://dotnet.microsoft.com/download)
- SQL Server (LocalDB or full instance)
- A configured connection string named `DefaultConnection` in `appsettings.json` under `FootballLeagueManagement.UI`

## Installation and Run

git clone https://github.com/RenaMehdiyeva88/FootballLeagueManagementSystem.git
cd FootballLeagueManagementSystem/FootballLeagueManagement.UI

Update the `DefaultConnection` string in `appsettings.json` to point to your SQL Server instance, then apply migrations and run:

dotnet ef database update
dotnet run

## Usage Example

After running the program, a menu will appear:

=== FOOTBALL LEAGUE MANAGER ===
--- DATA ENTRY ---
  1) Add Stadium
  2) Add Team
  3) Add Player
  4) Add Match
--- REPORTS ---
  5) View Team Status
  6) View League Table
  7) View Top Scoring Teams
  8) View Most Goals Conceded
  9) View Top Scorers
  0) Exit

Start by adding a stadium, then a team (linked to that stadium), then players and matches. Use the report options to view league statistics at any time.

## Project Structure

FootballLeagueManagementSystem/
├── FootballLeagueManagement.DAL/
│   ├── Context/LeagueDbContext.cs
│   ├── Models/                     # Team, Player, Stadium, Match
│   ├── Interfaces/                 # Repository interfaces
│   ├── Repositories/                # StadiumRepository, TeamRepository, PlayerRepository, MatchRepository
│   └── Migrations/
├── FootballLeagueManagement.BLL/
│   └── Services/                   # TeamService, PlayerService, StadiumService, MatchService
├── FootballLeagueManagement.UI/
│   ├── Program.cs                  # Entry point, DI and DB setup
│   └── MenuOperation.cs            # Console menu logic
├── dbo.Teams.Table.sql
├── dbo.Players.Table.sql
├── dbo.Stadiums.Table.sql
├── dbo.Matches.Table.sql
├── dbo.__EFMigrationsHistory.Table.sql
└── README.md

## Author

RenaMehdiyeva88 (https://github.com/RenaMehdiyeva88)
