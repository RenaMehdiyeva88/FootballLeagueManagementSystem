using FootballLeagueManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballLeagueManagement.DAL.Context
{
    public class LeagueDbContext : DbContext
    {
        public LeagueDbContext(DbContextOptions<LeagueDbContext> options)
            : base(options) { }

        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<Player> Players { get; set; } = null!;
        public DbSet<Stadium> Stadiums { get; set; } = null!;
        public DbSet<Match> Matches { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stadium>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired().HasMaxLength(100);
                entity.Property(s => s.Capacity).IsRequired();
                entity.HasIndex(s => s.Name).IsUnique();
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Name).IsRequired().HasMaxLength(100);
                entity.Property(t => t.Code).IsRequired();
                entity.HasIndex(t => t.Name).IsUnique();
                entity.HasIndex(t => t.Code).IsUnique();
                entity.HasOne(t => t.Stadium)
                      .WithMany(s => s.Teams)
                      .HasForeignKey(t => t.StadiumId)
                      .IsRequired(false)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.FullName).IsRequired();
                entity.Property(p => p.JerseyNumber).IsRequired();
                entity.HasIndex(p => new { p.TeamId, p.JerseyNumber }).IsUnique();
                entity.HasOne(p => p.Team)
                      .WithMany(t => t.Players)
                      .HasForeignKey(p => p.TeamId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.HasKey(m => m.Id);
                entity.Property(m => m.Week).IsRequired();
                entity.Property(m => m.HomeGoals).IsRequired();
                entity.Property(m => m.AwayGoals).IsRequired();
                entity.HasOne(m => m.HomeTeam)
                      .WithMany()
                      .HasForeignKey(m => m.HomeTeamId)
                      .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(m => m.AwayTeam)
                      .WithMany()
                      .HasForeignKey(m => m.AwayTeamId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}