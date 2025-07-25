using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FootballManager.Core.DTOs;
using FootballManager.Core.Entities;
using FootballManager.Core.Interfaces;
using FootballManager.Services.Services;
using FootballManager.API.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FootballManager.Tests.ServiceTests
{
    public class TeamServiceTests
    {
        private readonly Mock<IRepository<Team>> _teamRepoMock = new();
        private readonly Mock<IRepository<Player>> _playerRepoMock = new();
        private readonly Mock<IRepository<Core.Entities.Match>> _matchRepoMock = new();

        private readonly TeamService _service;

        public TeamServiceTests()
        {
            _service = new TeamService(_teamRepoMock.Object, _playerRepoMock.Object, _matchRepoMock.Object);
        }

        [Fact]  
        public async Task CreateTeamAsync_ShouldReturnCreatedTeamDto()
        {
            var dto = new CreateTeamDto
            {
                Name = "Test FC",
                Location = "Cairo",
                FoundedYear = 2000,
                Coach = "John Doe"
            };

            var createdTeam = new Team
            {
                Id = 1,
                Name = dto.Name,
                Location = dto.Location,
                FoundedYear = dto.FoundedYear,
                Coach = dto.Coach
            };

            _teamRepoMock.Setup(r => r.AddAsync(It.IsAny<Team>()))
                         .ReturnsAsync(createdTeam);

            var result = await _service.CreateTeamAsync(dto);

            Assert.Equal(1, result.Id);
            Assert.Equal(dto.Name, result.Name);
        }

        [Fact]
        public async Task DeleteTeamAsync_TeamNotFound_ReturnsFalse()
        {
            _teamRepoMock.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
                         .ReturnsAsync((Team)null);

            var result = await _service.DeleteTeamAsync(99);

            Assert.False(result);
        }
    }
}
