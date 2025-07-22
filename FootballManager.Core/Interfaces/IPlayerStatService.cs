using FootballManager.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballManager.Core.Interfaces
{
    public interface IPlayerStatService
    {
        Task<IEnumerable<PlayerStatDto>> GetStatsByPlayerIdAsync(int playerId);
        Task<IEnumerable<PlayerStatDto>> GetStatsByTeamIdAsync(int teamId);
        Task<PlayerStatDto> AddStatToMatchAsync(int matchId, CreatePlayerStatDto dto);
    }
}
