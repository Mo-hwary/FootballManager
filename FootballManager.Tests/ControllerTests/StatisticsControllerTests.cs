using FootballManager.API.Controllers;
using FootballManager.Core.DTOs;
using FootballManager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FootballManager.Tests.ControllerTests
{
    public class StatisticsControllerTests
    {
        private readonly Mock<IStatisticsService> _svc = new();
        private readonly StatisticsController _controller;

        public StatisticsControllerTests()
        {
            _controller = new StatisticsController(_svc.Object);
        }

        [Fact]
        public async Task GetPlayerStats_ReturnsOk()
        {
            _svc.Setup(s => s.GetStatsByPlayerIdAsync(10))
                .ReturnsAsync(new List<StatisticsDto>
                {
                    new StatisticsDto{ Id = 1, PlayerId = 10, Goals = 2 }
                });

            var result = await _controller.GetPlayerStats(10);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<StatisticsDto>>(ok.Value);
            Assert.Single(value);
        }

        [Fact]
        public async Task GetTeamStats_ReturnsOk()
        {
            _svc.Setup(s => s.GetStatsByTeamIdAsync(5))
                .ReturnsAsync(new List<StatisticsDto>
                {
                    new StatisticsDto{ Id = 1, PlayerId = 7, Goals = 1 },
                    new StatisticsDto{ Id = 0, PlayerId = 8, Goals = 0 } // لاعب بلا إحصائيات
                });

            var result = await _controller.GetTeamStats(5);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsAssignableFrom<IEnumerable<StatisticsDto>>(ok.Value);
            Assert.Equal(2, System.Linq.Enumerable.Count(value));
        }

        [Fact]
        public async Task AddPlayerStat_ReturnsOk()
        {
            var dto = new CreateStatisticsDto
            {
                PlayerId = 5,
                Goals = 1,
                Assists = 1,
                MinutesPlayed = 90,
                YellowCards = 0,
                RedCards = 0
            };

            _svc.Setup(s => s.AddStatToMatchAsync(3, dto))
                .ReturnsAsync(new StatisticsDto
                {
                    Id = 555,
                    PlayerId = 5,
                    Goals = 1
                });

            var result = await _controller.AddPlayerStat(3, dto);

            var ok = Assert.IsType<OkObjectResult>(result);
            var value = Assert.IsType<StatisticsDto>(ok.Value);
            Assert.Equal(555, value.Id);
        }
    }
}
