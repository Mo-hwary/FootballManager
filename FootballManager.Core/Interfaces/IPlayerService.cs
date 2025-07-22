using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FootballManager.Core.DTOs;

namespace FootballManager.Core.Interfaces
{
    public interface IPlayerService
    {
        Task<IEnumerable<PlayerDto>> GetAllPlayersAsync();
        Task<PlayerDto> GetPlayerByIdAsync(int id);
        Task<PlayerDto> CreatePlayerAsync(CreatePlayerDto dto);
        Task<bool> UpdatePlayerAsync(int id, CreatePlayerDto dto);
        Task<bool> DeletePlayerAsync(int id);
    }
}

