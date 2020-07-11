using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qldfuelanalyseapi.Models;

namespace qldfuelanalyseapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteFuelsController : ControllerBase
    {
        private readonly qldfuelContext _context;

        public SiteFuelsController(qldfuelContext context)
        {
            _context = context;
        }

        // GET: api/SiteFuels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiteFuel>>> GetSiteFuel()
        {
            return await _context.SiteFuel.ToListAsync();
        }

        // GET: api/SiteFuels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SiteFuel>> GetSiteFuel(int id)
        {
            var siteFuel = await _context.SiteFuel.FindAsync(id);

            if (siteFuel == null)
            {
                return NotFound();
            }

            return siteFuel;
        }

        // PUT: api/SiteFuels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSiteFuel(int id, SiteFuel siteFuel)
        {
            if (id != siteFuel.Id)
            {
                return BadRequest();
            }

            _context.Entry(siteFuel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteFuelExists(id))
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

        // POST: api/SiteFuels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SiteFuel>> PostSiteFuel(SiteFuel siteFuel)
        {
            _context.SiteFuel.Add(siteFuel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSiteFuel", new { id = siteFuel.Id }, siteFuel);
        }

        // DELETE: api/SiteFuels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SiteFuel>> DeleteSiteFuel(int id)
        {
            var siteFuel = await _context.SiteFuel.FindAsync(id);
            if (siteFuel == null)
            {
                return NotFound();
            }

            _context.SiteFuel.Remove(siteFuel);
            await _context.SaveChangesAsync();

            return siteFuel;
        }

        private bool SiteFuelExists(int id)
        {
            return _context.SiteFuel.Any(e => e.Id == id);
        }
    }
}
