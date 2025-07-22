using FootballManager.Core.DTOs;
using FootballManager.Core.Entities;
using FootballManager.Core.Interfaces;

namespace FootballManager.Services.Services
{
    public class PlayerStatService : IPlayerStatService
    {
        private readonly IRepository<PlayerStat> _statRepo;
        private readonly IRepository<Player> _playerRepo;

        public PlayerStatService(IRepository<PlayerStat> statRepo, IRepository<Player> playerRepo)
        {
            _statRepo = statRepo;
            _playerRepo = playerRepo;
        }

        public async Task<IEnumerable<PlayerStatDto>> GetStatsByPlayerIdAsync(int playerId)
        {
            var player = await _playerRepo.GetByIdAsync(playerId);
            if (player == null) return Enumerable.Empty<PlayerStatDto>();

            var allStats = await _statRepo.GetAllAsync();
            var stats = allStats.Where(s => s.PlayerId == playerId).ToList();

            return stats.Select(s => new PlayerStatDto
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

        public async Task<IEnumerable<PlayerStatDto>> GetStatsByTeamIdAsync(int teamId)
        {
            var allPlayers = await _playerRepo.GetAllAsync();
            var teamPlayers = allPlayers.Where(p => p.TeamId == teamId).ToList();

            if (!teamPlayers.Any()) return Enumerable.Empty<PlayerStatDto>();

            var playerIds = teamPlayers.Select(p => p.Id).ToHashSet();

            var allStats = await _statRepo.GetAllAsync();
            var stats = allStats.Where(s => playerIds.Contains(s.PlayerId)).ToList();

            return stats.Select(s =>
            {
                var player = teamPlayers.FirstOrDefault(p => p.Id == s.PlayerId);
                return new PlayerStatDto
                {
                    Id = s.Id,
                    PlayerId = s.PlayerId,
                    PlayerName = player?.Name,
                    MatchId = s.MatchId,
                    Goals = s.GoalsScored,
                    Assists = s.Assists,
                    MinutesPlayed = s.MinutesPlayed,
                    YellowCards = s.YellowCards,
                    RedCards = s.RedCards
                };
            });
        }

        public async Task<PlayerStatDto> AddStatToMatchAsync(int matchId, CreatePlayerStatDto dto)
        {
            var player = await _playerRepo.GetByIdAsync(dto.PlayerId);
            if (player == null)
                throw new ArgumentException($"Player with ID {dto.PlayerId} not found.");


            var stat = new PlayerStat
            {
                PlayerId = dto.PlayerId,
                MatchId = matchId,
                GoalsScored = dto.Goals,
                Assists = dto.Assists,
                MinutesPlayed = dto.MinutesPlayed,
                YellowCards = dto.YellowCards,
                RedCards = dto.RedCards
            };

            var created = await _statRepo.AddAsync(stat);

            return new PlayerStatDto
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
