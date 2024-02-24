﻿using System;
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
    public class StatsController : ControllerBase
    {
        private readonly GobtrackerDbContext _context;

        public StatsController(GobtrackerDbContext context)
        {
            _context = context;
        }

        // GET: api/Stats
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Stat>>> GetStats()
        {
          if (_context.Stats == null)
          {
              return NotFound();
          }
            return await _context.Stats.ToListAsync();
        }

        // GET: api/Stats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Stat>> GetStat(int id)
        {
          if (_context.Stats == null)
          {
              return NotFound();
          }
            var stat = await _context.Stats.FindAsync(id);

            if (stat == null)
            {
                return NotFound();
            }

            return stat;
        }

        // PUT: api/Stats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStat(int id, Stat stat)
        {
            if (id != stat.Id)
            {
                return BadRequest();
            }

            _context.Entry(stat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatExists(id))
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

        // POST: api/Stats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Stat>> PostStat(Stat stat)
        {
          if (_context.Stats == null)
          {
              return Problem("Entity set 'GobtrackerDbContext.Stats'  is null.");
          }
            _context.Stats.Add(stat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStat", new { id = stat.Id }, stat);
        }

        // DELETE: api/Stats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStat(int id)
        {
            if (_context.Stats == null)
            {
                return NotFound();
            }
            var stat = await _context.Stats.FindAsync(id);
            if (stat == null)
            {
                return NotFound();
            }

            _context.Stats.Remove(stat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatExists(int id)
        {
            return (_context.Stats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
