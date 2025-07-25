using FootballManager.Core.DTOs;
using FootballManager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FootballManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statService;

        public StatisticsController(IStatisticsService statService)
        {
            _statService = statService;
        }

        [HttpGet("players/{id}/stats")]
        public async Task<IActionResult> GetPlayerStats(int id)
        {
            var stats = await _statService.GetStatsByPlayerIdAsync(id);
            return Ok(stats);
        }

        [HttpGet("teams/{teamId}/stats")]
        public async Task<IActionResult> GetTeamStats(int teamId)
        {
            var stats = await _statService.GetStatsByTeamIdAsync(teamId);
            return Ok(stats);
        }

        [HttpPost("matches/{matchId}/stats")]
        public async Task<IActionResult> AddPlayerStat(int matchId, [FromBody] CreateStatisticsDto dto)
        {
            var created = await _statService.AddStatToMatchAsync(matchId, dto);
            return Ok(created);
        }
    }
}
