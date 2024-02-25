using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GOBTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GOBTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamRosterController : ControllerBase
    {
        private readonly GobtrackerDbContext _context;

        public TeamRosterController(GobtrackerDbContext context)
        {
            _context = context;
        }

        // GET:
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamRoster>>> GetTeamRosters()
        {
            if (_context.TeamRosters == null)
            {
                return NotFound();
            }
            return await _context.TeamRosters.ToListAsync();
        }

        // GET: 
        [HttpGet("{teamID}")]
        public async Task<ActionResult<IEnumerable<TeamRoster>>> GetTeamRosters(int teamID)
        {
            if (_context.TeamRosters == null)
            {
                return NotFound();
            }
            var allRoster = await _context.TeamRosters.ToListAsync();


            if (allRoster == null)
            {
                return NotFound();
            }

            var teamRoster = allRoster.Where(x => x.TeamId == teamID).ToList();

            return teamRoster;
        }

    }
}
