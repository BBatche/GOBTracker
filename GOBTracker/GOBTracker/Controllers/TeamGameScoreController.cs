using GOBTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GOBTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamGameScoreController : ControllerBase
    {
        private readonly GobtrackerDbContext _context;

        public TeamGameScoreController(GobtrackerDbContext context)
        {
            _context = context;
        }

        // GET:
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamGameScore>>> GetAllTeamGameScores()
        {
            if (_context.TeamGameScores == null)
            {
                return NotFound();
            }
            return await _context.TeamGameScores.ToListAsync();
        }

        // GET: 
        [HttpGet("game/{gameID}")]
        public async Task<ActionResult<IEnumerable<TeamGameScore>>> GetAllTeamGameScoresByGameId(int gameID)
        {
            if (_context.TeamGameScores == null)
            {
                return NotFound();
            }
            var allStats = await _context.TeamGameScores.ToListAsync();


            if (allStats == null)
            {
                return NotFound();
            }

            var gameScores = allStats.Where(x => x.GameId == gameID).ToList();

            return gameScores;
        }

        
    }
}
 
