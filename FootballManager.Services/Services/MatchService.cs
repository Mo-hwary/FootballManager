using FootballManager.Core.DTOs;
using FootballManager.Core.Entities;
using FootballManager.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballManager.Services.Services
{
    public class MatchService : IMatchService
    {
        private readonly IRepository<Match> _matchRepository;
        private readonly IRepository<Team> _teamRepository;

        public MatchService(IRepository<Match> matchRepository, IRepository<Team> teamRepository)
        {
            _matchRepository = matchRepository;
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<MatchDto>> GetAllMatchesAsync()
        {
            var matches = await _matchRepository.GetAllAsync();
            var teams = await _teamRepository.GetAllAsync();

            return matches.Select(m => new MatchDto
            {
                Id = m.Id,
                Date = m.Date,
                HomeTeamId = m.HomeTeamId,
                AwayTeamId = m.AwayTeamId,
                HomeTeamScore = m.HomeTeamScore,
                AwayTeamScore = m.AwayTeamScore,
                Stadium = m.Stadium,
                HomeTeamName = teams.FirstOrDefault(t => t.Id == m.HomeTeamId)?.Name ?? "",
                AwayTeamName = teams.FirstOrDefault(t => t.Id == m.AwayTeamId)?.Name ?? ""
            });
        }

        public async Task<IEnumerable<MatchDto>> GetMatchesByTeamIdAsync(int teamId)
        {
            var matches = await _matchRepository.GetAllAsync();
            var teams = await _teamRepository.GetAllAsync();

            var filtered = matches
                .Where(m => m.HomeTeamId == teamId || m.AwayTeamId == teamId)
                .ToList();

            return filtered.Select(m => new MatchDto
            {
                Id = m.Id,
                Date = m.Date,
                HomeTeamId = m.HomeTeamId,
                AwayTeamId = m.AwayTeamId,
                HomeTeamScore = m.HomeTeamScore,
                AwayTeamScore = m.AwayTeamScore,
                Stadium = m.Stadium,
                HomeTeamName = teams.FirstOrDefault(t => t.Id == m.HomeTeamId)?.Name ?? "",
                AwayTeamName = teams.FirstOrDefault(t => t.Id == m.AwayTeamId)?.Name ?? ""
            });
        }

        public async Task<MatchDto> CreateMatchAsync(CreateMatchDto dto)
        {
            var homeTeam = await _teamRepository.GetByIdAsync(dto.HomeTeamId);
            var awayTeam = await _teamRepository.GetByIdAsync(dto.AwayTeamId);

            if (homeTeam == null || awayTeam == null)
                throw new System.Exception("One or both teams not found.");

            var match = new Match
            {
                Date = dto.Date,
                HomeTeamId = dto.HomeTeamId,
                AwayTeamId = dto.AwayTeamId,
                HomeTeamScore = dto.HomeTeamScore,
                AwayTeamScore = dto.AwayTeamScore,
                Stadium = dto.Stadium
            };

            var created = await _matchRepository.AddAsync(match);

            return new MatchDto
            {
                Id = created.Id,
                Date = created.Date,
                HomeTeamId = created.HomeTeamId,
                AwayTeamId = created.AwayTeamId,
                HomeTeamScore = created.HomeTeamScore,
                AwayTeamScore = created.AwayTeamScore,
                Stadium = created.Stadium,
                HomeTeamName = homeTeam.Name,
                AwayTeamName = awayTeam.Name
            };
        }
    }
}
