using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballManager.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace FootballManager.Infrastructure.Data;
public class FootballDbContext : DbContext
{
    public FootballDbContext(DbContextOptions<FootballDbContext> options)
        : base(options) { }

    public DbSet<Team> Teams { get; set; }
    public DbSet<Player> Players { get; set; }
    public DbSet<Match> Matches { get; set; }
    public DbSet<PlayerStat> PlayerStats { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Team>()
            .HasIndex(t => t.Name)
            .IsUnique();

        modelBuilder.Entity<Team>()
            .HasMany(t => t.HomeMatches)
            .WithOne(m => m.HomeTeam)
            .HasForeignKey(m => m.HomeTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Team>()
            .HasMany(t => t.AwayMatches)
            .WithOne(m => m.AwayTeam)
            .HasForeignKey(m => m.AwayTeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Player>()
            .HasOne(p => p.Team)
            .WithMany(t => t.Players)
            .HasForeignKey(p => p.TeamId);

        modelBuilder.Entity<PlayerStat>()
            .HasOne(ps => ps.Player)
            .WithMany(p => p.PlayerStats)
            .HasForeignKey(ps => ps.PlayerId);

        modelBuilder.Entity<PlayerStat>()
            .HasOne(ps => ps.Match)
            .WithMany(m => m.PlayerStats)
            .HasForeignKey(ps => ps.MatchId);
    }
}
