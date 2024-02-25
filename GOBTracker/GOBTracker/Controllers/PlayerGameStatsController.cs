using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GOBTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GOBTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerGameStatsController : ControllerBase
    {
        private readonly GobtrackerDbContext _context;

        public PlayerGameStatsController(GobtrackerDbContext context)
        {
            _context = context;
        }

        // GET:
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerGameStat>>> GetAllStats()
        {
            if (_context.PlayerGameStats == null)
            {
                return NotFound();
            }
            return await _context.PlayerGameStats.ToListAsync();
        }

        // GET: 
        [HttpGet("player/{playerID}")]
        public async Task<ActionResult<IEnumerable<PlayerGameStat>>> GetAllStatsByPlayerId(int playerID)
        {
            if (_context.PlayerGameStats == null)
            {
                return NotFound();
            }
            var allStats = await _context.PlayerGameStats.ToListAsync();


            if (allStats == null)
            {
                return NotFound();
            }

            var playerStats = allStats.Where(x => x.PlayerId == playerID).ToList();

            return playerStats;
        }

        // GET: 
        [HttpGet("game/{gameID}")]
        public async Task<ActionResult<IEnumerable<PlayerGameStat>>> GetAllStatsByGameId(int gameID)
        {
            if (_context.PlayerGameStats == null)
            {
                return NotFound();
            }
            var allStats = await _context.PlayerGameStats.ToListAsync();


            if (allStats == null)
            {
                return NotFound();
            }

            var gameStats = allStats.Where(x => x.GameId == gameID).ToList();

            return gameStats;
        }
    }
}
