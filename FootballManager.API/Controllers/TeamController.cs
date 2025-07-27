using FootballManager.Core.DTOs;
using FootballManager.Core.Entities;
using FootballManager.Core.InterfacesServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FootballManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Team>> GetTeamByName(string name)
        {
            var team = await _teamService.GetTeamByNameAsync(name);
            if (team == null)
                return NotFound($"Team with name '{name}' not found.");

            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto dto)
        {
            var created = await _teamService.CreateTeamAsync(dto);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] CreateTeamDto dto)
        {
            var updated = await _teamService.UpdateTeamAsync(id, dto);
            if (!updated)
                return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var success = await _teamService.DeleteTeamAsync(id);
            if (!success)
                return NotFound($"Team with ID '{id}' not found.");

            return NoContent();
        }
    }
}
