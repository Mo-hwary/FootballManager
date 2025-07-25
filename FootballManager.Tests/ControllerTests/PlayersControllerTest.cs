using FootballManager.API.Controllers;
using FootballManager.Core.DTOs;
using FootballManager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FootballManager.Tests
{
    public class PlayersControllerTests
    {
        private readonly Mock<IPlayerService> _playerServiceMock = new();
        private readonly PlayersController _controller;

        public PlayersControllerTests()
        {
            _controller = new PlayersController(_playerServiceMock.Object);
        }

        [Fact]
        public async Task GetAllPlayers_ShouldReturnOkWithPlayers()
        {
            var players = new List<PlayerDto>
    {
        new PlayerDto { Id = 1, Name = "Salah", Position = "Forward", TeamId = 1 }
    };

            _playerServiceMock.Setup(s => s.GetAllPlayersAsync())
                              .ReturnsAsync(players);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<PlayerDto>>(okResult.Value);
            Assert.Single(returnValue);
        }

    }
}
