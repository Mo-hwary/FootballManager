using FootballManager.Core.DTOs;
using FootballManager.Core.Entities;
using FootballManager.Core.Interfaces;
using FootballManager.Core.InterfacesRepo;
using FootballManager.Infrastructure.Repositories;
using System.Numerics;

namespace FootballManager.Services.Services
{
    public class StatisticsService(IStatsRepo statsRepo, IRepository<Player> playerRepo) : IStatisticsService
    {
       
        public async Task<IEnumerable<StatisticsDto>> GetStatsByPlayerIdAsync(int playerId)
        {
            var player = await playerRepo.GetByIdAsync(playerId);
            if (player == null)
                return Enumerable.Empty<StatisticsDto>();

            var allStats = await statsRepo.GetAllAsync();
            var stats = allStats.Where(s => s.PlayerId == playerId).ToList();

            if (!stats.Any())
            {
                return new List<StatisticsDto>
        {
            new StatisticsDto
            {
                PlayerId = player.Id,
                PlayerName = player.Name,
                MatchId = 0,
                Goals = 0,
                Assists = 0,
                MinutesPlayed = 0,
                YellowCards = 0,
                RedCards = 0
            }
        };
            }

            return stats.Select(s => new StatisticsDto
            {
                Id = s.Id,
                PlayerId = s.PlayerId,
                PlayerName = player.Name,
                MatchId = s.MatchId,
                Goals = s.GoalsScored,
                Assists = s.Assists,
                MinutesPlayed = s.MinutesPlayed,
                YellowCards = s.YellowCards,
                RedCards = s.RedCards
            });

        }
        public async Task<IEnumerable<StatisticsDto>> GetStatsByTeamIdAsync(int teamId)
        {
            var stat = await statsRepo.GetAllStatAsync(teamId);

            if (!stat.Any())
            {
                var players = await playerRepo.GetAllAsync();
                return players
                    .Where(p => p.TeamId == teamId)
                    .Select(p => new StatisticsDto
                    {
                        PlayerId = p.Id,
                        PlayerName = p.Name,
                        TeamId=p.TeamId,
                        MatchId = 0,
                        Goals = 0,
                        Assists = 0,
                        MinutesPlayed = 0,
                        YellowCards = 0,
                        RedCards = 0
                    });
            }

            return stat.Select(s => new StatisticsDto
            {

                Id = s.Id,
                PlayerId = s.PlayerId,
                PlayerName = s.Player.Name,
                MatchId = s.MatchId,
                Goals = s.GoalsScored,
                Assists = s.Assists,
                MinutesPlayed = s.MinutesPlayed,
                YellowCards = s.YellowCards,
                RedCards = s.RedCards
            });
         
        }

        public async Task<StatisticsDto> AddStatToMatchAsync(int matchId, CreateStatisticsDto dto)
        {
            var player = await playerRepo.GetByIdAsync(dto.PlayerId);
            if (player == null)
                throw new ArgumentException($"Player with ID {dto.PlayerId} not found.");

            var stat = new Statistics
            {
                PlayerId = dto.PlayerId,
                MatchId = matchId,
                GoalsScored = dto.Goals,
                Assists = dto.Assists,
                MinutesPlayed = dto.MinutesPlayed,
                YellowCards = dto.YellowCards,
                RedCards = dto.RedCards
            };

            var created = await statsRepo.AddAsync(stat);

            return new StatisticsDto
            {
                Id = created.Id,
                PlayerId = created.PlayerId,
                MatchId = created.MatchId,
                Goals = created.GoalsScored,
                Assists = created.Assists,
                MinutesPlayed = created.MinutesPlayed,
                YellowCards = created.YellowCards,
                RedCards = created.RedCards
            };
        }
    }
}
