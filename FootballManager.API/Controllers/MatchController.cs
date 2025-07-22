using FootballManager.Core.DTOs;
using FootballManager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FootballManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matches = await _matchService.GetAllMatchesAsync();
            return Ok(matches);
        }

        [HttpGet("{TeamId}")]
        public async Task<IActionResult> GetByTeamId(int TeamId)
        {
            var match = await _matchService.GetMatchesByTeamIdAsync(TeamId);
            if (match == null) return NotFound();
            return Ok(match);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateMatchDto dto)
        {
            var created = await _matchService.CreateMatchAsync(dto);
            return CreatedAtAction(nameof(GetByTeamId), new { teamId = created.HomeTeamId }, created);

        }

    }
}
