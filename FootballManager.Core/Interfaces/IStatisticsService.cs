using FootballManager.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballManager.Core.Interfaces
{
    public interface IStatisticsService
    {
        Task<IEnumerable<StatisticsDto>> GetStatsByPlayerIdAsync(int playerId);
        Task<IEnumerable<StatisticsDto>> GetStatsByTeamIdAsync(int teamId);
        Task<StatisticsDto> AddStatToMatchAsync(int matchId, CreateStatisticsDto dto);
    }
}
