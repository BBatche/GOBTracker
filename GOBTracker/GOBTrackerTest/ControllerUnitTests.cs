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

        // ---------------- add schedules, stats, StatTypes, TeamRosters ------------------------------
        //controllers!
        private PlayerGameStatsController playerGameStatsController;
        private PlayersController playersController;
        private GamesController gamesController;
        private TeamsController teamController;
        private PlayerTeamsController playerTeamsController;
        //Our mock db!
        private GobtrackerDbContext fakeDbContext;

        // player stats ( need them in form of list and dbset)
        private DbSet<PlayerGameStat> fakePlayerGameStatsDbSet;

        //lists!
        private List<PlayerGameStat> fakePlayerGameStateList;
        private List<Player> fakePlayerList;
        private List<Game> fakeGameList;
        private List<OpponentTeamGameStat> fakeOpponentTeamGameStatsList;
        private List<Team> fakeTeamList;
        private List<PlayerTeam> fakePlayerTeamList;

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





            //////////////////// ------------------- Setup Data for other controller B ----------------------
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
            fakePlayerList.Add(new Player
            {
                Id = 101,
                FirstName = "Dyldog",
                LastName = "Concat"
            });
            var mockStatsPlayer = fakePlayerList.AsQueryable().BuildMockDbSet();

            A.CallTo(() => fakeDbContext.Players).Returns(mockStatsPlayer);
            //////////////////// ------------------------ Setup Data for other controller C ---------------------------

            fakeGameList = new List<Game>();
            fakeGameList.Add(new Game
            {
                Id = 11,
                OurTeamId = 11,
                OpponentTeamId = 12,
                Location = "University of Pittsburgh - Batch//e",
                GameDateTime = DateTimeOffset.UtcNow
            });
            fakeGameList.Add(new Game
            {
                Id = 6,
                OurTeamId = 11,
                OpponentTeamId = 19,
                Location = "University of Bofa",
                GameDateTime = DateTimeOffset.UtcNow
            });
            fakeGameList.Add(new Game
            {
                Id = 68,
                OurTeamId = 11,
                OpponentTeamId = 12,
                Location = "University of Yippee",
                GameDateTime = DateTimeOffset.UtcNow
            });
            var mockStatsGame = fakeGameList.AsQueryable().BuildMockDbSet();

            A.CallTo(() => fakeDbContext.Games).Returns(mockStatsGame);

            ////////////////////---------------------------- Setup Data for other controller D ----------------------------


            fakeTeamList = new List<Team>();
            fakeTeamList.Add(new Team
            {
                Id = 11,
                TeamName = "Tests",
            });

            fakeTeamList.Add(new Team
            {
                Id = 12,
                TeamName = "Bobbies",
            });
            fakeTeamList.Add(new Team
            {
                Id = 19,
                TeamName = "Blakies",
            });
            var mockTeam = fakeTeamList.AsQueryable().BuildMockDbSet();

            A.CallTo(() => fakeDbContext.Teams).Returns(mockTeam);



            //////////////////// Setup Data for other controller E

            fakePlayerTeamList = new List<PlayerTeam>();
            fakePlayerTeamList.Add(new PlayerTeam
            {
                Id = 4,
                PlayerId = 69,
                TeamId = 19
            });
            fakePlayerTeamList.Add(new PlayerTeam
            {
                Id = 5,
                PlayerId = 21,
                TeamId = 12
            });
            fakePlayerTeamList.Add(new PlayerTeam
            {
                Id = 0,
                PlayerId = 101,
                TeamId = 11
            });
            var mockPlayerTeam = fakePlayerTeamList.AsQueryable().BuildMockDbSet();

            A.CallTo(() => fakeDbContext.PlayerTeams).Returns(mockPlayerTeam);


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
        [TestMethod]
        public async Task GetGames_ReturnsOkResult()
        {
            // not much you can do here except ensure ok is returned, not null, and correct number of items, and no exceptions
            try
            {
                gamesController = new GamesController(fakeDbContext);
                var result = await gamesController.GetGames();
                Assert.IsNotNull(result);
                //Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
                Assert.IsTrue(result.Value.ToList().Count == fakeGameList.Count);  // sort of ensures that none are filtered

                // on more advanced controllers, you can ensure filtering is correct
            }
            catch (Exception ex)
            {
                Assert.Fail($"Exception occurred: {ex.Message}");
            }

        }
        // Controller D Test 


        [TestMethod]
        public async Task GetTeams_ReturnsOkResult()
        {
            // not much you can do here except ensure ok is returned, not null, and correct number of items, and no exceptions
            try
            {
                teamController = new TeamsController(fakeDbContext);
                var result = await teamController.GetTeams();
                Assert.IsNotNull(result);
                //Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
                Assert.IsTrue(result.Value.ToList().Count == fakeTeamList.Count);  // sort of ensures that none are filtered

                // on more advanced controllers, you can ensure filtering is correct
            }
            catch (Exception ex)
            {
                Assert.Fail($"Exception occurred: {ex.Message}");
            }

        }
        // Controller D Test 
        [TestMethod]
        public async Task GetPlayerTeams_ReturnsOkResult()
        {
            // not much you can do here except ensure ok is returned, not null, and correct number of items, and no exceptions
            try
            {
                playerTeamsController = new PlayerTeamsController(fakeDbContext);
                var result = await playerTeamsController.GetPlayerTeams();
                Assert.IsNotNull(result);
                //Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
                Assert.IsTrue(result.Value.ToList().Count == fakePlayerTeamList.Count);  // sort of ensures that none are filtered


            }
            catch (Exception ex)
            {
                Assert.Fail($"Exception occurred: {ex.Message}");
            }

        }
    }
}

