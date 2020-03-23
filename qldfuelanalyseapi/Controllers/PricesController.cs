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
    public class PricesController : ControllerBase
    {
        private readonly qldfuelContext _context;

        public PricesController(qldfuelContext context)
        {
            _context = context;
        }

        // GET: api/Prices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prices>>> GetPrices()
        {
            return await _context.Prices.ToListAsync();
        }

        // GET: api/Prices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prices>> GetPrices(int id)
        {
            var prices = await _context.Prices.FindAsync(id);

            if (prices == null)
            {
                return NotFound();
            }

            return prices;
        }

        private bool PricesExists(int id)
        {
            return _context.Prices.Any(e => e.TransactionId == id);
        }
    }
}
