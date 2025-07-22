using FootballManager.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballManager.Core.Interfaces
{
    public interface IMatchService
    {

        Task<IEnumerable<MatchDto>> GetAllMatchesAsync();
        Task<IEnumerable<MatchDto>> GetMatchesByTeamIdAsync(int TeamId);
        Task<MatchDto> CreateMatchAsync(CreateMatchDto dto);

    }
}
