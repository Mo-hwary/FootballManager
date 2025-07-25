using FootballManager.Core.DTOs;
using FootballManager.Core.Entities;
using FootballManager.Core.Interfaces;
using FootballManager.Services.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Match = FootballManager.Core.Entities.Match;

namespace FootballManager.Tests.ServiceTests
{
    public class MatchServiceTests
    {
        private readonly Mock<IRepository<Match>> _matchRepoMock = new();
        private readonly Mock<IRepository<Team>> _teamRepoMock = new();

        private readonly MatchService _service;

        public MatchServiceTests()
        {
            _service = new MatchService(_matchRepoMock.Object, _teamRepoMock.Object);
        }

        [Fact]
        public async Task GetAllMatchesAsync_ShouldReturnMatchDtos()
        {
            // Arrange
            var matches = new List<Match>
            {
                new Match { Id = 1, HomeTeamId = 1, AwayTeamId = 2, HomeTeamScore = 2, AwayTeamScore = 1, Date = DateTime.Today, Stadium = "Main" }
            };

            var teams = new List<Team>
            {
                new Team { Id = 1, Name = "Team A" },
                new Team { Id = 2, Name = "Team B" }
            };

            _matchRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(matches);
            _teamRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(teams);

            // Act
            var result = await _service.GetAllMatchesAsync();

            // Assert
            var first = result.First();
            Assert.Single(result);
            Assert.Equal("Team A", first.HomeTeamName);
            Assert.Equal("Team B", first.AwayTeamName);
        }

        [Fact]
        public async Task GetMatchesByTeamIdAsync_ShouldFilterByTeam()
        {
            // Arrange
            var matches = new List<Match>
            {
                new Match { Id = 1, HomeTeamId = 5, AwayTeamId = 2, HomeTeamScore = 1, AwayTeamScore = 0, Date = DateTime.Today, Stadium = "S1" },
                new Match { Id = 2, HomeTeamId = 3, AwayTeamId = 5, HomeTeamScore = 2, AwayTeamScore = 2, Date = DateTime.Today, Stadium = "S2" },
                new Match { Id = 3, HomeTeamId = 7, AwayTeamId = 8, HomeTeamScore = 3, AwayTeamScore = 1, Date = DateTime.Today, Stadium = "S3" }
            };

            var teams = new List<Team>
            {
                new Team { Id = 2, Name = "T2" },
                new Team { Id = 3, Name = "T3" },
                new Team { Id = 5, Name = "T5" },
                new Team { Id = 7, Name = "T7" },
                new Team { Id = 8, Name = "T8" }
            };

            _matchRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(matches);
            _teamRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(teams);

            // Act
            var result = await _service.GetMatchesByTeamIdAsync(5);

            // Assert
            Assert.Equal(2, result.Count());
            Assert.All(result, m => Assert.True(m.HomeTeamId == 5 || m.AwayTeamId == 5));
        }

        [Fact]
        public async Task CreateMatchAsync_ShouldThrow_WhenHomeOrAwayNotFound()
        {
            // Arrange
            var dto = new CreateMatchDto
            {
                Date = DateTime.Now,
                HomeTeamId = 1,
                AwayTeamId = 2,
                HomeTeamScore = 0,
                AwayTeamScore = 0,
                Stadium = "Camp Nou"
            };

            _teamRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Team)null!);
            _teamRepoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(new Team { Id = 2, Name = "Away" });

            // Act + Assert
            await Assert.ThrowsAsync<Exception>(() => _service.CreateMatchAsync(dto));
        }

        [Fact]
        public async Task CreateMatchAsync_ShouldReturnDto_WhenOk()
        {
            // Arrange
            var dto = new CreateMatchDto
            {
                Date = DateTime.Now,
                HomeTeamId = 1,
                AwayTeamId = 2,
                HomeTeamScore = 3,
                AwayTeamScore = 1,
                Stadium = "Camp Nou"
            };

            _teamRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Team { Id = 1, Name = "Home" });
            _teamRepoMock.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(new Team { Id = 2, Name = "Away" });

            _matchRepoMock.Setup(r => r.AddAsync(It.IsAny<Match>()))
                          .ReturnsAsync((Match m) =>
                          {
                              m.Id = 10;
                              return m;
                          });

            // Act
            var result = await _service.CreateMatchAsync(dto);

            // Assert
            Assert.Equal(10, result.Id);
            Assert.Equal("Home", result.HomeTeamName);
            Assert.Equal("Away", result.AwayTeamName);
            Assert.Equal(3, result.HomeTeamScore);
            Assert.Equal(1, result.AwayTeamScore);
        }
    }
}
