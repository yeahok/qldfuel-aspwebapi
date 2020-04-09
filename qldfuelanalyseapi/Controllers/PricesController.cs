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
        public async Task<ActionResult<IEnumerable<Prices>>> GetPrices(string fueltype)
        {
            return await _context.Prices.Where(p => p.FuelType == fueltype).ToListAsync();
        }

        [HttpGet("Latest/{id}")]
        public async Task<ActionResult<List<Prices>>> GetLatestPrices(int id)
        {
            var prices = new List<Prices>();
            var fuelTypes = await _context.Prices.Where(p => p.SiteId == id).Select(e => e.FuelType).Distinct().ToListAsync();
            foreach (string fuelt in fuelTypes)
            {
                var price = await _context.Prices.Where(p => p.SiteId == id && p.FuelType == fuelt)
                    .OrderByDescending(s => s.TransactionDateutc).Take(1).SingleAsync();
                prices.Add(price);
            }
            return prices;
        }

        // GET: api/Prices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Prices>>> GetPrices(int id, string fueltype)
        {
            var prices = await _context.Prices.Where(p => p.SiteId == id && p.FuelType == fueltype).ToListAsync();

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
