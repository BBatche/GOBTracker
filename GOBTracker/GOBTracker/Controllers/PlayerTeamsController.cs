using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GOBTracker.Models;
using System.Linq;

namespace GOBTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerTeamsController : ControllerBase
    {
        private readonly GobtrackerDbContext _context;

        public PlayerTeamsController(GobtrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/PlayerTeams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerTeam>>> GetPlayerTeams()
        {
          if (_context.PlayerTeams == null)
          {
              return NotFound();
          }
            return await _context.PlayerTeams.ToListAsync();
        }

        // GET: api/PlayerTeams/5
        [HttpGet("id/{id}")]
        public async Task<ActionResult<PlayerTeam>> GetPlayerTeamByID(int id)
        {
          if (_context.PlayerTeams == null)
          {
              return NotFound();
          }
            var playerTeam = await _context.PlayerTeams.FindAsync(id);

            if (playerTeam == null)
            {
                return NotFound();
            }

            return playerTeam;
        }

        [HttpGet("player/{playerID}")]
        public async Task<ActionResult<IEnumerable<PlayerTeam>>> GetPlayerTeamByPlayerID(int playerID)
        {
            if (_context.PlayerTeams == null)
            {
                return NotFound();
            }
            var playerTeam = await _context.PlayerTeams.ToListAsync();

            if (playerTeam == null)
            {
                return NotFound();
            }
            playerTeam = playerTeam.Where(x => x.PlayerId == playerID).ToList();

            return playerTeam;
        }


        // PUT: api/PlayerTeams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerTeam(int id, PlayerTeam playerTeam)
        {
            if (id != playerTeam.Id)
            {
                return BadRequest();
            }

            _context.Entry(playerTeam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerTeamExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PlayerTeams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayerTeam>> PostPlayerTeam(PlayerTeam playerTeam)
        {
          if (_context.PlayerTeams == null)
          {
              return Problem("Entity set 'GobtrackerDbContext.PlayerTeams'  is null.");
          }
            _context.PlayerTeams.Add(playerTeam);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerTeams", new { id = playerTeam.Id }, playerTeam);  // wow

        }

        // DELETE: api/PlayerTeams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerTeam(int id)
        {
            if (_context.PlayerTeams == null)
            {
                return NotFound();
            }
            var playerTeam = await _context.PlayerTeams.FindAsync(id);
            if (playerTeam == null)
            {
                return NotFound();
            }

            _context.PlayerTeams.Remove(playerTeam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerTeamExists(int id)
        {
            return (_context.PlayerTeams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
