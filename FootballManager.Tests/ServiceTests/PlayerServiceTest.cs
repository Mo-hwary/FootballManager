using FootballManager.Core.DTOs;
using FootballManager.Core.Entities;
using FootballManager.Core.Interfaces;
using FootballManager.Services.Services;
using Moq;
using Xunit;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace FootballManager.Tests.ServiceTests
{
    public class PlayerServiceTests
    {
        private readonly Mock<IRepository<Player>> _playerRepoMock = new();
        private readonly Mock<IRepository<Team>> _teamRepoMock = new();
        private readonly PlayerService _service;

        public PlayerServiceTests()
        {
            _service = new PlayerService(_playerRepoMock.Object, _teamRepoMock.Object);
        }

        [Fact]
        public async Task CreatePlayerAsync_ShouldReturnCreatedPlayerDto()
        {
            var dto = new CreatePlayerDto
            {
                Name = "Salah",
                DateOfBirth = new System.DateOnly(1992, 6, 15),
                Nationality = "Egypt",
                Position = "Forward",
                TeamId = 1
            };

            var player = new Player
            {
                Id = 1,
                Name = dto.Name,
                TeamId = dto.TeamId,
                DateOfBirth = dto.DateOfBirth,
                Nationality = dto.Nationality,
                Position = dto.Position
            };

            _playerRepoMock.Setup(p => p.AddAsync(It.IsAny<Player>())).ReturnsAsync(player);

            var result = await _service.CreatePlayerAsync(dto);

            Assert.Equal(1, result.Id);
            Assert.Equal(dto.Name, result.Name);
        }
    }
}
