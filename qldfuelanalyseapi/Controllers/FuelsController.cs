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
    public class FuelsController : ControllerBase
    {
        private readonly qldfuelContext _context;

        public FuelsController(qldfuelContext context)
        {
            _context = context;
        }

        // GET: api/Fuels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fuel>>> GetFuel()
        {
            return await _context.Fuel.ToListAsync();
        }

        // GET: api/Fuels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fuel>> GetFuel(int id)
        {
            var fuel = await _context.Fuel.FindAsync(id);

            if (fuel == null)
            {
                return NotFound();
            }

            return fuel;
        }

        private bool FuelExists(int id)
        {
            return _context.Fuel.Any(e => e.Id == id);
        }
    }
}
