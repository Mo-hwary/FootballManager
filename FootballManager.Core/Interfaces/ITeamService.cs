using FootballManager.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballManager.Core.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
        Task<TeamDto> GetTeamByNameAsync(string name);
        Task<TeamDto> CreateTeamAsync(CreateTeamDto dto);
        Task<bool> UpdateTeamAsync(int id, CreateTeamDto dto);
        Task<bool> DeleteTeamAsync(int id);
    }
}
