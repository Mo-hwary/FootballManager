using FootballManager.Core.DTOs;
using FootballManager.Core.Entities;
using FootballManager.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FootballManager.Services.Services
{
    public class TeamService : ITeamService
    {
        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<Player> _playerRepository;
        private readonly IRepository<Match> _matchRepository;

        public TeamService(
            IRepository<Team> teamRepository,
            IRepository<Player> playerRepository,
            IRepository<Match> matchRepository)
        {
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
            _matchRepository = matchRepository;
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            return teams.Select(t => new TeamDto
            {
                Id = t.Id,
                Name = t.Name,
                Location = t.Location,
                FoundedYear = t.FoundedYear,
                Coach = t.Coach
            });
        }

        public async Task<TeamDto> GetTeamByNameAsync(string name)
        {
            var team = await _teamRepository.FindAsync(t => t.Name.ToLower() == name.ToLower());
            if (team == null) return null;

            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                Location = team.Location,
                FoundedYear = team.FoundedYear,
                Coach = team.Coach
            };
        }

        public async Task<TeamDto> CreateTeamAsync(CreateTeamDto dto)
        {
            var team = new Team
            {
                Name = dto.Name,
                Location = dto.Location,
                FoundedYear = dto.FoundedYear,
                Coach = dto.Coach
            };

            var created = await _teamRepository.AddAsync(team);

            return new TeamDto
            {
                Id = created.Id,
                Name = created.Name,
                Location = created.Location,
                FoundedYear = created.FoundedYear,
                Coach = created.Coach
            };
        }

        public async Task<bool> UpdateTeamAsync(int id, CreateTeamDto dto)
        {
            var existingTeam = await _teamRepository.GetByIdAsync(id);
            if (existingTeam == null)
                return false;

            existingTeam.Name = dto.Name;
            existingTeam.Location = dto.Location;
            existingTeam.FoundedYear = dto.FoundedYear;
            existingTeam.Coach = dto.Coach;

            await _teamRepository.UpdateAsync(existingTeam);
            return true;
        }

        public async Task<bool> DeleteTeamAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team == null)
                return false;

            var players = await _playerRepository.GetAllAsync();
            var teamPlayers = players.Where(p => p.TeamId == id);
            foreach (var player in teamPlayers)
            {
                await _playerRepository.DeleteAsync(player);
            }

            var matches = await _matchRepository.GetAllAsync();
            var teamMatches = matches.Where(m => m.HomeTeamId == id || m.AwayTeamId == id);
            foreach (var match in teamMatches)
            {
                await _matchRepository.DeleteAsync(match);
            }

            await _teamRepository.DeleteAsync(team);
            return true;
        }
    }
}
