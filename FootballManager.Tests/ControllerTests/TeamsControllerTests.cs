using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballManager.Core.DTOs;
using FootballManager.Core.Entities;
using FootballManager.Services.Services;
using FootballManager.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FootballManager.Core.InterfacesServices;

namespace FootballManager.Tests
{

    public class TeamsControllerTests
    {
        private readonly Mock<ITeamService> _teamServiceMock = new();
        private readonly TeamsController _controller;

        public TeamsControllerTests()
        {
            _controller = new TeamsController(_teamServiceMock.Object);
        }

        [Fact]
        public async Task GetAllTeams_ReturnsOkWithTeams()
        {
            // Arrange
            var teams = new List<TeamDto>
    {
        new TeamDto { Id = 1, Name = "Ahly", Location = "Cairo", FoundedYear = 1907, Coach = "Koller" }
    };

            _teamServiceMock.Setup(s => s.GetAllTeamsAsync())
                            .ReturnsAsync(teams);

            // Act
            var actionResult = await _controller.GetAllTeams();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<TeamDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

    }
}
