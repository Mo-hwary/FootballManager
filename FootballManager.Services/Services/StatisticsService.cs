using FootballManager.Core.DTOs;
using FootballManager.Core.Entities;
using FootballManager.Core.Interfaces;

namespace FootballManager.Services.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IRepository<Statistics> _statRepo;
        private readonly IRepository<Player> _playerRepo;

        public StatisticsService(IRepository<Statistics> statRepo, IRepository<Player> playerRepo)
        {
            _statRepo = statRepo;
            _playerRepo = playerRepo;
        }

        public async Task<IEnumerable<StatisticsDto>> GetStatsByPlayerIdAsync(int playerId)
        {
            var player = await _playerRepo.GetByIdAsync(playerId);
            if (player == null)
                return Enumerable.Empty<StatisticsDto>();

            var allStats = await _statRepo.GetAllAsync();
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
            var allPlayers = await _playerRepo.GetAllAsync();
            var teamPlayers = allPlayers.Where(p => p.TeamId == teamId).ToList();

            if (!teamPlayers.Any()) return Enumerable.Empty<StatisticsDto>();

            var allStats = await _statRepo.GetAllAsync();

            var result = new List<StatisticsDto>();

            foreach (var player in teamPlayers)
            {
                var playerStats = allStats.Where(s => s.PlayerId == player.Id).ToList();

                if (playerStats.Any())
                {
                    result.AddRange(playerStats.Select(s => new StatisticsDto
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
                    }));
                }
                else
                {
                    // إحصائية فاضية لو اللاعب مالوش أي إحصائيات
                    result.Add(new StatisticsDto
                    {
                        Id = 0,
                        PlayerId = player.Id,
                        PlayerName = player.Name,
                        MatchId = 0,
                        Goals = 0,
                        Assists = 0,
                        MinutesPlayed = 0,
                        YellowCards = 0,
                        RedCards = 0
                    });
                }
            }

            return result;
        }

        public async Task<StatisticsDto> AddStatToMatchAsync(int matchId, CreateStatisticsDto dto)
        {
            var player = await _playerRepo.GetByIdAsync(dto.PlayerId);
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

            var created = await _statRepo.AddAsync(stat);

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
