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
            // Arrange

            // Act
            var result = await _controller.GetGames();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult.Value);
            Assert.IsInstanceOfType(okResult.Value, typeof(List<Game>));
            var games = okResult.Value as List<Game>;
            Assert.AreEqual(0, games.Count);
        }

        [TestMethod]
        public async Task GetGame_WithNonExistentId_ReturnsNotFound()
        {
            // Arrange

            // Act
            var result = await _controller.GetGame(0);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        

    }
}