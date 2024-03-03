using GOBTracker.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GOBTracker.Models;
using Microsoft.Extensions.DependencyInjection;



namespace GOBTrackerTest
{
    [TestClass]
    public class GamesControllerTests
    {
        
        private GamesController _controller;
        private GobtrackerDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            HttpClient client = new HttpClient();
        }

        [TestMethod]
        public async Task GetGames_ReturnsEmptyList()
        {
            // Act
            var result = await _controller.GetGames();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Value, typeof(List<Game>));
            Assert.AreEqual(0, result.Value.Count());
        }

        [TestMethod]
        public async Task GetGame_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            int nonExistentId = 999;

            // Act
            var result = await _controller.GetGame(nonExistentId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task AddGame_ReturnsCreated()
        {
            // Arrange
            var newGame = new Game
            {
                Id = 1,
                OurTeamId = 1,
                OpponentTeamId = 2,
                GameDateTime = DateTimeOffset.Now,
                Location = "Test Location",
                OurTeam = new Team { Id = 1, TeamName = "Our Team" },
                OpponentTeam = new Team { Id = 2, TeamName = "Opponent Team" }
            };

            // Act
            var result = await _controller.PostGame(newGame);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(CreatedAtActionResult));
        }

        [TestMethod]
        public async Task UpdateGame_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            int nonExistentId = 999;
            var updatedGame = new Game
            {
                Id = nonExistentId,
                OurTeamId = 1,
                OpponentTeamId = 2,
                GameDateTime = DateTimeOffset.Now,
                Location = "Updated Location",
                OurTeam = new Team { Id = 1, TeamName = "Our Team" },
                OpponentTeam = new Team { Id = 2, TeamName = "Opponent Team" }
            };

            // Act
            var result = await _controller.PutGame(nonExistentId, updatedGame);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteGame_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange
            int nonExistentId = 999;

            // Act
            var result = await _controller.DeleteGame(nonExistentId);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}