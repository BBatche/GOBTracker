using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GOBTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace GOBTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly GobtrackerDbContext _context;

        public SchedulesController(GobtrackerDbContext context)
        {
            _context = context;
        }

        // GET:
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetTeamSchedules()
        {
            if (_context.Schedules == null)
            {
                return NotFound();
            }
            return await _context.Schedules.ToListAsync();
        }

        // GET: 
        [HttpGet("{teamID}")]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetTeamRosters(int teamID)
        {
            if (_context.Schedules == null)
            {
                return NotFound();
            }
            var allSchedules = await _context.Schedules.ToListAsync();


            if (allSchedules == null)
            {
                return NotFound();
            }

            var teamSchedule = allSchedules.Where(x => x.OurTeamId == teamID || x.OpponentTeamId == teamID).ToList();

            return teamSchedule;
        }
    }
}
