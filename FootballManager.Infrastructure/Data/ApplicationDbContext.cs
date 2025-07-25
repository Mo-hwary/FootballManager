using FootballManager.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballManager.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Player> Players => Set<Player>();
        public DbSet<Match> Matches => Set<Match>();
        public DbSet<Statistics> PlayerStats => Set<Statistics>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Team - Players
            modelBuilder.Entity<Team>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<Team>()
                .HasMany(t => t.Players)
                .WithOne(p => p.Team)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Team - HomeMatches
            modelBuilder.Entity<Match>()
                .HasOne(m => m.HomeTeam)
                .WithMany(t => t.HomeMatches)
                .HasForeignKey(m => m.HomeTeamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Team - AwayMatches
            modelBuilder.Entity<Match>()
                .HasOne(m => m.AwayTeam)
                .WithMany(t => t.AwayMatches)
                .HasForeignKey(m => m.AwayTeamId)
                .OnDelete(DeleteBehavior.Cascade);

            // Match - PlayerStats
            modelBuilder.Entity<Match>()
                .HasMany(m => m.PlayerStats)
                .WithOne(ps => ps.Match)
                .HasForeignKey(ps => ps.MatchId)
                .OnDelete(DeleteBehavior.Cascade);

            // Player - PlayerStats
            modelBuilder.Entity<Player>()
                .HasMany(p => p.PlayerStats)
                .WithOne(ps => ps.Player)
                .HasForeignKey(ps => ps.PlayerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
