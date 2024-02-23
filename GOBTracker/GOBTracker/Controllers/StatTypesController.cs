using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GOBTracker.Models;

namespace GOBTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatTypesController : ControllerBase
    {
        private readonly GameOfBasketballDbContext _context;

        public StatTypesController(GameOfBasketballDbContext context)
        {
            _context = context;
        }

        // GET: api/StatTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatType>>> GetStatTypes()
        {
          if (_context.StatTypes == null)
          {
              return NotFound();
          }
            return await _context.StatTypes.ToListAsync();
        }

        // GET: api/StatTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatType>> GetStatType(int id)
        {
          if (_context.StatTypes == null)
          {
              return NotFound();
          }
            var statType = await _context.StatTypes.FindAsync(id);

            if (statType == null)
            {
                return NotFound();
            }

            return statType;
        }

        // PUT: api/StatTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatType(int id, StatType statType)
        {
            if (id != statType.Id)
            {
                return BadRequest();
            }

            _context.Entry(statType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatTypeExists(id))
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

        // POST: api/StatTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatType>> PostStatType(StatType statType)
        {
          if (_context.StatTypes == null)
          {
              return Problem("Entity set 'GameOfBasketballDbContext.StatTypes'  is null.");
          }
            _context.StatTypes.Add(statType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatType", new { id = statType.Id }, statType);
        }

        // DELETE: api/StatTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatType(int id)
        {
            if (_context.StatTypes == null)
            {
                return NotFound();
            }
            var statType = await _context.StatTypes.FindAsync(id);
            if (statType == null)
            {
                return NotFound();
            }

            _context.StatTypes.Remove(statType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatTypeExists(int id)
        {
            return (_context.StatTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
