using FootballManager.API.Controllers;
using FootballManager.Core.DTOs;
using FootballManager.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FootballManager.Tests.ControllerTests
{
    public class MatchesControllerTests
    {
        private readonly Mock<IMatchService> _svc = new();
        private readonly MatchesController _controller;

        public MatchesControllerTests()
        {
            _controller = new MatchesController(_svc.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOk_WithMatches()
        {
            // Arrange
            _svc.Setup(s => s.GetAllMatchesAsync())
                .ReturnsAsync(new List<MatchDto>
                {
                    new MatchDto{ Id = 1, HomeTeamId = 1, AwayTeamId = 2, HomeTeamName = "A", AwayTeamName = "B" }
                });

            // Act
            var result = await _controller.GetAll();

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<MatchDto>>(ok.Value);
            Assert.Single(list);
        }

        [Fact]
        public async Task GetByTeamId_ReturnsOk_WithFilteredMatches()
        {
            _svc.Setup(s => s.GetMatchesByTeamIdAsync(5))
                .ReturnsAsync(new List<MatchDto>
                {
                    new MatchDto{ Id = 1, HomeTeamId = 5, AwayTeamId = 2 },
                    new MatchDto{ Id = 2, HomeTeamId = 3, AwayTeamId = 5 },
                });

            var result = await _controller.GetByTeamId(5);

            var ok = Assert.IsType<OkObjectResult>(result);
            var list = Assert.IsAssignableFrom<IEnumerable<MatchDto>>(ok.Value);
            Assert.Equal(2, System.Linq.Enumerable.Count(list));
        }

        [Fact]
        public async Task Create_ReturnsCreatedAt_WhenSuccess()
        {
            var dto = new CreateMatchDto
            {
                Date = DateTime.Now,
                HomeTeamId = 1,
                AwayTeamId = 2,
                HomeTeamScore = 1,
                AwayTeamScore = 0,
                Stadium = "Stadium"
            };

            _svc.Setup(s => s.CreateMatchAsync(dto))
                .ReturnsAsync(new MatchDto
                {
                    Id = 100,
                    Date = dto.Date,
                    HomeTeamId = 1,
                    AwayTeamId = 2,
                    HomeTeamScore = 1,
                    AwayTeamScore = 0,
                    Stadium = "Stadium",
                    HomeTeamName = "H",
                    AwayTeamName = "A"
                });

            var result = await _controller.Create(dto);

            var created = Assert.IsType<CreatedAtActionResult>(result);
            var value = Assert.IsType<MatchDto>(created.Value);
            Assert.Equal(100, value.Id);
        }
    }
}
