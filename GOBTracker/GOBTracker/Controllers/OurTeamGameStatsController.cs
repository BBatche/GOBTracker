using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GOBTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GOBTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OurTeamGameStatsController : ControllerBase
    {
        private readonly GobtrackerDbContext _context;

        public OurTeamGameStatsController(GobtrackerDbContext context)
        {
            _context = context;
        }

        [HttpGet("{gameID}")]
        public async Task<ActionResult<IEnumerable<OurTeamGameStat>>> GetAllStatsByPlayerId(int gameID)
        {
            if (_context.OurTeamGameStats == null)
            {
                return NotFound();
            }
            var fullTeamGameStats = await _context.OurTeamGameStats.ToListAsync();


            if (fullTeamGameStats == null)
            {
                return NotFound();
            }

            var ourTeamStats = fullTeamGameStats.Where(x => x.GameId == gameID).ToList();

            return ourTeamStats;
        }
    }
}
