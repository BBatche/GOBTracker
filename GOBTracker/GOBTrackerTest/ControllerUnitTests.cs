using GOBTracker.Controllers;
using GOBTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MockQueryable;
using MockQueryable.FakeItEasy;

namespace GOBTrackerTest
{
    [TestClass]
    public class GamesControllerTests
    {

        // Dependency Injection - allows a class's dependency to be injected at runtime
        // The controllers need a context.  In normal cases that is connected to the DB
        // However, you want to test your controllers without needing the DB so that only
        // the Controller code is tested.  It will also not touch data in your live db.
        // We use dependency injection libraries like FakeItEasy or Moq to achieve this.

        //controllers!
        private PlayerGameStatsController playerGameStatsController;
        private PlayersController playersController;

        //Our mock db!
        private GobtrackerDbContext fakeDbContext;

        // player stats ( need them in form of list and dbset)
        private DbSet<PlayerGameStat> fakePlayerGameStatsDbSet;

        //lists!
        private List<PlayerGameStat> fakePlayerGameStateList;
        private List<Player> fakePlayerList;
        [TestInitialize]
        public void Setup()
        {


            // A fake db Context
            fakeDbContext = A.Fake<GobtrackerDbContext>();

            //////////////////// Setup Data for playerGameStatsController

            // Make a list of stats that will be returned when we get stats
            fakePlayerGameStateList = new List<PlayerGameStat>();
            fakePlayerGameStateList.Add(new PlayerGameStat
            {
                FirstName = "Drake",
                LastName = "Flopper",
                GameDateTime = DateTimeOffset.UtcNow,
                Total2ptsMade = 1,
                Total3ptsMade = 0,
                TotalPoints = 2,

                // Oh how I wish there were free throws here
                GameId = 1,
                PlayerId = 1
            });
            fakePlayerGameStateList.Add(new PlayerGameStat
            {
                FirstName = "Pryce",
                LastName = "Scratchy",
                GameDateTime = DateTimeOffset.UtcNow,
                Total2ptsMade = 100,
                Total3ptsMade = 1,
                TotalPoints = 203,

                // Oh how I wish there were free throws here
                GameId = 1,
                PlayerId = 1
            });

            // convert the list of stats to a dbset because EF returns that
            var mockStats = fakePlayerGameStateList.AsQueryable().BuildMockDbSet();


            // specify that 
            // A call to PlayerGameStats will return the mockStats
            A.CallTo(() => fakeDbContext.PlayerGameStats).Returns(mockStats);

            //////////////////// Setup Data for other controller B
            fakePlayerList = new List<Player>();
            fakePlayerList.Add(new Player
            {
                Id = 69,
                FirstName = "Balake",
                LastName = "Whopper"
            });
            fakePlayerList.Add(new Player
            {
                Id = 21,
                FirstName = "Salmon",
                LastName = "Fish"
            });

            var mockStatsPlayer = fakePlayerList.AsQueryable().BuildMockDbSet();

            A.CallTo(() => fakeDbContext.Players).Returns(mockStatsPlayer);
            //////////////////// Setup Data for other controller C


            //////////////////// Setup Data for other controller D


            //////////////////// Setup Data for other controller E

        }

        [TestMethod]
        public async Task GetAllStats_ReturnsOkResult()
        {
            // not much you can do here except ensure ok is returned, not null, and correct number of items, and no exceptions
            try
            {
                playerGameStatsController = new PlayerGameStatsController(fakeDbContext);
                var result = await playerGameStatsController.GetAllStats();
                Assert.IsNotNull(result);
                //Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
                Assert.IsTrue(result.Value.ToList().Count == fakePlayerGameStateList.Count);  // sort of ensures that none are filtered
                
                // on more advanced controllers, you can ensure filtering is correct
            }
            catch (Exception ex)
            {
                Assert.Fail($"Exception occurred: {ex.Message}");
            }

        }

        // Controller B Test 
        [TestMethod]
        public async Task GetPlayers_ReturnsOkResult()
        {
            // not much you can do here except ensure ok is returned, not null, and correct number of items, and no exceptions
            try
            {
                playersController = new PlayersController(fakeDbContext);
                var result = await playersController.GetPlayers();
                Assert.IsNotNull(result);
                //Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
                Assert.IsTrue(result.Value.ToList().Count == fakePlayerList.Count);  // sort of ensures that none are filtered

                // on more advanced controllers, you can ensure filtering is correct
            }
            catch (Exception ex)
            {
                Assert.Fail($"Exception occurred: {ex.Message}");
            }

        }
        // Controller C Test 

        // Controller D Test 

        // Controller D Test 

    }
}
