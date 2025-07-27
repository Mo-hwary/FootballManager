using FootballManager.Core.DTOs;
using FootballManager.Core.Entities;
using FootballManager.Core.Interfaces;
using FootballManager.Services.Services;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FootballManager.Tests.ServiceTests
{
    public class StatisticsServiceTests
    {
        private readonly Mock<IRepository<Statistics>> _statRepoMock = new();
        private readonly Mock<IRepository<Player>> _playerRepoMock = new();

        private readonly StatisticsService _service;

        public StatisticsServiceTests(IStatsRepo statsRepo)
        {
        _service= new StatisticsService(statsRepo, _playerRepoMock.Object);
        }
        
        [Fact]
        public async Task GetStatsByPlayerIdAsync_PlayerNotFound_ReturnsEmpty()
        {
            _playerRepoMock.Setup(r => r.GetByIdAsync(999))
                           .ReturnsAsync((Player)null!);

            var result = await _service.GetStatsByPlayerIdAsync(999);

            Assert.Empty(result);
        }

        [Fact]
        public async Task GetStatsByPlayerIdAsync_ReturnsMappedDtos()
        {
            var player = new Player { Id = 10, Name = "Messi", TeamId = 1 };
            var stats = new List<Statistics>
            {
                new Statistics { Id = 1, PlayerId = 10, MatchId = 5, GoalsScored = 2, Assists = 1, MinutesPlayed = 90, YellowCards = 0, RedCards = 0 }
            };

            _playerRepoMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(player);
            _statRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(stats);

            var result = await _service.GetStatsByPlayerIdAsync(10);

            var first = Assert.Single(result);
            Assert.Equal(2, first.Goals);
            Assert.Equal("Messi", first.PlayerName);
        }

        [Fact]
        public async Task GetStatsByTeamIdAsync_ReturnsPlayersWithOrWithoutStats()
        {
            var players = new List<Player>
            {
                new Player { Id = 1, TeamId = 5, Name = "A" },
                new Player { Id = 2, TeamId = 5, Name = "B" } 
            };

            var stats = new List<Statistics>
            {
                new Statistics { Id = 100, PlayerId = 1, MatchId = 3, GoalsScored = 1, Assists = 0, MinutesPlayed = 90, YellowCards = 1, RedCards = 0 }
            };

            _playerRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(players);
            _statRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(stats);

            var result = await _service.GetStatsByTeamIdAsync(5);

            Assert.Equal(2, result.Count());
            Assert.Contains(result, r => r.PlayerId == 2 && r.Goals == 0);
        }

        [Fact]
        public async Task AddStatToMatchAsync_ShouldThrow_IfPlayerNotFound()
        {
            _playerRepoMock.Setup(r => r.GetByIdAsync(50)).ReturnsAsync((Player)null!);

            var dto = new CreateStatisticsDto
            {
                PlayerId = 50,
                Goals = 1,
                Assists = 1,
                MinutesPlayed = 90,
                YellowCards = 0,
                RedCards = 0
            };

            await Assert.ThrowsAsync<System.ArgumentException>(() => _service.AddStatToMatchAsync(3, dto));
        }

        [Fact]
        public async Task AddStatToMatchAsync_ShouldReturnDto_WhenOk()
        {
            var player = new Player { Id = 7, Name = "X" };
            _playerRepoMock.Setup(r => r.GetByIdAsync(7)).ReturnsAsync(player);

            _statRepoMock.Setup(r => r.AddAsync(It.IsAny<Statistics>()))
                         .ReturnsAsync((Statistics s) =>
                         {
                             s.Id = 999;
                             return s;
                         });

            var dto = new CreateStatisticsDto
            {
                PlayerId = 7,
                Goals = 2,
                Assists = 1,
                MinutesPlayed = 88,
                YellowCards = 0,
                RedCards = 0
            };

            var result = await _service.AddStatToMatchAsync(3, dto);

            Assert.Equal(999, result.Id);
            Assert.Equal(7, result.PlayerId);
            Assert.Equal(2, result.Goals);
        }
    }
}
