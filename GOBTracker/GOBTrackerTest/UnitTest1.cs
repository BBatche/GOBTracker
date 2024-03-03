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
        private PlayerGameStatsController _playerGameStatsController;
        private PlayersController _playersController;
        private GamesController _gamesController;
        private GobtrackerDbContext _context;

        [TestInitialize]
        public void Setup()
        {
            HttpClient client = new HttpClient();
        }

        [TestMethod]
        public async Task PlayersController_GetPlayers_ReturnsOkResult()
        {
            // Arrange

            // Act
            var result = await _playersController.GetPlayers();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task PlayersController_GetPlayer_ReturnsNotFoundResult()
        {
            // Arrange
            int nonExistentPlayerId = 999;

            // Act
            var result = await _playersController.GetPlayer(nonExistentPlayerId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GamesController_GetGames_ReturnsOkResult()
        {
            // Arrange

            // Act
            var result = await _gamesController.GetGames();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
        }

        [TestMethod]
        public async Task GamesController_GetGame_ReturnsNotFoundResult()
        {
            // Arrange
            int nonExistentGameId = 999;

            // Act
            var result = await _gamesController.GetGame(nonExistentGameId);

            // Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
    }
}