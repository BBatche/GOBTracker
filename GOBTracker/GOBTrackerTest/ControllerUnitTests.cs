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

        // ---------------- add TeamRosters ------------------------------
        //controllers!
        private PlayerGameStatsController playerGameStatsController;
        private PlayersController playersController;
        private GamesController gamesController;
        private TeamsController teamController;
        private PlayerTeamsController playerTeamsController;
        private SchedulesController schedulesController;
        private StatsController statsController;
        private StatTypesController statTypesController;
        private TeamRosterController TeamRosterController;

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
        private List<Schedule> fakeScheduleList;
        private List<Stat> fakeStatList;
        private List<StatType> fakeStatTypeList;
        private List<TeamRoster> fakeTeamRosterList;

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
                Total2ptsMade = 20,
                Total2ptsMissed = 1,
                Total3ptsMade = 4,
                Total3ptsMissed = 0,
                TotalPoints = 60,
                TotalSteals = 2,
                TotalFtMade = 8,
                TotalTurnovers = 0,
                TotalAssists = 9,
                TotalBlocks = 10,
                TotalFouls = 0,
                TotalOffensiveRebounds = 0,
                TotalDefensiveRebounds = 2,

                TotalFtMissed = 0,

                GameId = 11,
                PlayerId = 69
            });
            fakePlayerGameStateList.Add(new PlayerGameStat
            {
                FirstName = "Pryce",
                LastName = "Scratchy",
                GameDateTime = DateTimeOffset.UtcNow,
                Total2ptsMade = 1,
                Total2ptsMissed = 1,
                Total3ptsMade = 0,
                Total3ptsMissed = 0,
                TotalPoints = 10,
                TotalSteals = 2,
                TotalFtMade = 8,
                TotalTurnovers = 0,
                TotalAssists = 0,
                TotalBlocks = 0,
                TotalFouls = 0,
                TotalOffensiveRebounds = 0,
                TotalDefensiveRebounds = 2,

                TotalFtMissed = 0,

                GameId = 11,
                PlayerId = 21
            });
            fakePlayerGameStateList.Add(new PlayerGameStat
            {
                FirstName = "Dyldog",
                LastName = "Concat",
                GameDateTime = DateTimeOffset.UtcNow,
                Total2ptsMade = 5,
                Total2ptsMissed = 2,
                Total3ptsMade = 2,
                Total3ptsMissed = 1,
                TotalPoints = 17,
                TotalSteals = 2,
                TotalFtMade = 1,
                TotalTurnovers = 0,
                TotalAssists = 2,
                TotalBlocks = 4,
                TotalFouls = 0,
                TotalOffensiveRebounds = 0,
                TotalDefensiveRebounds = 2,

                TotalFtMissed = 0,

                GameId = 6,
                PlayerId = 101
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
                FirstName = "Drake",
                LastName = "Flopper"
            });
            fakePlayerList.Add(new Player
            {
                Id = 21,
                FirstName = "Pryce",
                LastName = "Scratchy"
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
                OurTeamId = 19,
                OpponentTeamId = 12,
                Location = "University of Pittsburgh - Batch//e",
                GameDateTime = DateTimeOffset.UtcNow
            });
            fakeGameList.Add(new Game
            {
                Id = 6,
                OurTeamId = 19,
                OpponentTeamId = 11,
                Location = "University of Bofa",
                GameDateTime = DateTimeOffset.UtcNow
            });
            fakeGameList.Add(new Game
            {
                Id = 68,
                OurTeamId = 19,
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



            //////////////////// --------------- Setup Data for other controller E ------------------------------

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

            // --------------------------------- Set up FOR CONTROLLER F ------------------------------

            fakeScheduleList = new List<Schedule>();
            fakeScheduleList.Add(new Schedule
            {
                OurTeam = "Blakies",
                Opponent = "Bobbies",
                GameDateTime = DateTimeOffset.UtcNow,
                Location = "Maui, Hawaii",
                OurTeamId = 19,
                OpponentTeamId = 12,
                GameId = 11
            });
            fakeScheduleList.Add(new Schedule
            {
                OurTeam = "Blakies",
                Opponent = "Bobbies",
                GameDateTime = DateTimeOffset.UtcNow,
                Location = "Honolulu, Hawaii",
                OurTeamId = 19,
                OpponentTeamId = 12,
                GameId = 0
            });
            fakeScheduleList.Add(new Schedule
            {
                OurTeam = "Blakies",
                Opponent = "Tests",
                GameDateTime = DateTimeOffset.UtcNow,
                Location = "Saudi Arabia",
                OurTeamId = 19,
                OpponentTeamId = 11,
                GameId = 6
            });
            var mockSchedule = fakeScheduleList.AsQueryable().BuildMockDbSet();

            A.CallTo(() => fakeDbContext.Schedules).Returns(mockSchedule);


            // --------------------------------- Set up FOR CONTROLLER G ------------------------------


            fakeStatList = new List<Stat>();
            fakeStatList.Add(new Stat
            {
                Id = 0,
                PlayerTeamId = 19,
                GameId = 11,
                StatTypeId = 0,
                StatValue = 20,
            });
            fakeStatList.Add(new Stat
            {
                Id = 1,
                PlayerTeamId = 19,
                GameId = 11,
                StatTypeId = 1,
                StatValue = 1,
            });
            var mockStat = fakeStatList.AsQueryable().BuildMockDbSet();

            A.CallTo(() => fakeDbContext.Stats).Returns(mockStat);


            // --------------------------------- Set up FOR CONTROLLER H ------------------------------

            fakeStatTypeList = new List<StatType>();
            fakeStatTypeList.Add(new StatType
            {
                Id = 0,
                StatName = "2PointMade",
                StatNameAbr = "2PtMd",
            });
            fakeStatTypeList.Add(new StatType
            {
                Id = 1,
                StatName = "2PointMiss",
                StatNameAbr = "2PtMs",
            });
            var mockStatType = fakeStatTypeList.AsQueryable().BuildMockDbSet();

            A.CallTo(() => fakeDbContext.StatTypes).Returns(mockStatType);


            // --------------------------------- Set up FOR CONTROLLER I ------------------------------

            fakeTeamRosterList = new List<TeamRoster>();
            fakeTeamRosterList.Add(new TeamRoster
            {
                Id = 0,
                PlayerId = 69,
                TeamId = 19,
                FirstName = "Drake",
                LastName = "Flopper",
                TeamName = "Blakies"
            });
            fakeTeamRosterList.Add(new TeamRoster
            {
                Id = 1,
                PlayerId = 21,
                TeamId = 12,
                FirstName = "Pryce",
                LastName = "Scratchy",
                TeamName = "Bobbies"
            });


            var mockTeamRoster = fakeTeamRosterList.AsQueryable().BuildMockDbSet();

            A.CallTo(() => fakeDbContext.TeamRosters).Returns(mockTeamRoster);


        }
    //------------------------------------ TEST METHODS --------------------------------------------------------------

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

        // --------------------------------- Set up FOR UNIT TEST F ------------------------------

        [TestMethod]
        public async Task GetSchedules_ReturnsOkResult()
        {
            // not much you can do here except ensure ok is returned, not null, and correct number of items, and no exceptions
            try
            {
                schedulesController = new SchedulesController(fakeDbContext);
                var result = await schedulesController.GetTeamSchedules();
                Assert.IsNotNull(result);
                //Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
                Assert.IsTrue(result.Value.ToList().Count == fakeScheduleList.Count);  // sort of ensures that none are filtered


            }
            catch (Exception ex)
            {
                Assert.Fail($"Exception occurred: {ex.Message}");
            }

        }
        // --------------------------------- Set up FOR UNIT TEST G ------------------------------

        [TestMethod]
        public async Task GetStats_ReturnsOkResult()
        {
            // not much you can do here except ensure ok is returned, not null, and correct number of items, and no exceptions
            try
            {
                statsController = new StatsController(fakeDbContext);
                var result = await statsController.GetStats();
                Assert.IsNotNull(result);
                //Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
                Assert.IsTrue(result.Value.ToList().Count == fakeStatList.Count);  // sort of ensures that none are filtered


            }
            catch (Exception ex)
            {
                Assert.Fail($"Exception occurred: {ex.Message}");
            }
        }

        // --------------------------------- Set up FOR UNIT TEST H ------------------------------

        [TestMethod]
        public async Task GetStatsType_ReturnsOkResult()
        {
            // not much you can do here except ensure ok is returned, not null, and correct number of items, and no exceptions
            try
            {
                statTypesController = new StatTypesController(fakeDbContext);
                var result = await statTypesController.GetStatTypes();
                Assert.IsNotNull(result);
                //Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
                Assert.IsTrue(result.Value.ToList().Count == fakeStatTypeList.Count);  // sort of ensures that none are filtered


            }
            catch (Exception ex)
            {
                Assert.Fail($"Exception occurred: {ex.Message}");
            }
        }

        // --------------------------------- Set up FOR UNIT TEST I ------------------------------

        [TestMethod]
        public async Task GetTeamRosters_ReturnsOkResult()
        {
            // not much you can do here except ensure ok is returned, not null, and correct number of items, and no exceptions
            try
            {
                TeamRosterController = new TeamRosterController(fakeDbContext);
                var result = await TeamRosterController.GetTeamRosters();
                Assert.IsNotNull(result);
                //Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
                Assert.IsTrue(result.Value.ToList().Count == fakeTeamRosterList.Count);  // sort of ensures that none are filtered


            }
            catch (Exception ex)
            {
                Assert.Fail($"Exception occurred: {ex.Message}");
            }
        }

    }
}