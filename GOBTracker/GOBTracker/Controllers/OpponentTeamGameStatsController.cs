using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GOBTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GOBTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpponentTeamGameStats : ControllerBase
    {
        private readonly GobtrackerDbContext _context;

        public OpponentTeamGameStats(GobtrackerDbContext context)
        {
            _context = context;
        }

        [HttpGet("{gameID}")]
        public async Task<ActionResult<IEnumerable<OpponentTeamGameStat>>> GetAllStatsByPlayerId(int gameID)
        {
            if (_context.OpponentTeamGameStats == null)
            {
                return NotFound();
            }
            var fullTeamGameStats = await _context.OpponentTeamGameStats.ToListAsync();


            if (fullTeamGameStats == null)
            {
                return NotFound();
            }

            var oppTeamStats = fullTeamGameStats.Where(x => x.GameId == gameID).ToList();

            return oppTeamStats;
        }
    }
}
