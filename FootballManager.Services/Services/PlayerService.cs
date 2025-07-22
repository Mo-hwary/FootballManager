using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballManager.Core.DTOs;
using FootballManager.Core.Entities;
using FootballManager.Core.Interfaces;

namespace FootballManager.Services.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepository<Player> _playerRepository;
        private readonly IRepository<Team> _teamRepository;

        public PlayerService(IRepository<Player> playerRepository, IRepository<Team> teamRepository)
        {
            _playerRepository = playerRepository;
            _teamRepository = teamRepository;
        }

        public async Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            var result = new List<PlayerDto>();

            foreach (var p in players)
            {
                var team = await _teamRepository.GetByIdAsync(p.TeamId);

                result.Add(new PlayerDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    DateOfBirth = p.DateOfBirth,
                    Nationality = p.Nationality,
                    Position = p.Position,
                    TeamId = p.TeamId,
                    TeamName = team?.Name
                });
            }

            return result;
        }

        public async Task<PlayerDto> GetPlayerByIdAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player == null) return null;

            var team = await _teamRepository.GetByIdAsync(player.TeamId);

            return new PlayerDto
            {
                Id = player.Id,
                Name = player.Name,
                DateOfBirth = player.DateOfBirth,
                Nationality = player.Nationality,
                Position = player.Position,
                TeamId = player.TeamId,
                TeamName = team?.Name
            };
        }

        public async Task<PlayerDto> CreatePlayerAsync(CreatePlayerDto dto)
        {
            var player = new Player
            {
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                Position = dto.Position,
                TeamId = dto.TeamId
            };

            var created = await _playerRepository.AddAsync(player);

            var team = await _teamRepository.GetByIdAsync(created.TeamId);

            return new PlayerDto
            {
                Id = created.Id,
                Name = created.Name,
                DateOfBirth = created.DateOfBirth,
                Nationality = created.Nationality,
                Position = created.Position,
                TeamId = created.TeamId,
                TeamName = team?.Name
            };
        }

        public async Task<bool> UpdatePlayerAsync(int id, CreatePlayerDto dto)
        {
            var existing = await _playerRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Name = dto.Name;
            existing.DateOfBirth = dto.DateOfBirth;
            existing.Nationality = dto.Nationality;
            existing.Position = dto.Position;
            existing.TeamId = dto.TeamId;

            await _playerRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeletePlayerAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player == null) return false;

            await _playerRepository.DeleteAsync(player);
            return true;
        }
    }
}
