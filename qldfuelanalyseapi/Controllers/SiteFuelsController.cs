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

        private bool SiteFuelExists(int id)
        {
            return _context.SiteFuel.Any(e => e.Id == id);
        }
    }
}
