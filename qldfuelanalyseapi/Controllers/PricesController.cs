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
        public async Task<ActionResult<IEnumerable<Prices>>> GetPrices(string fueltype, string from, string to, int daterange = 14)
        {
            DateTime fromDate;
            DateTime toDate;

            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
            {
                fromDate = DateTime.ParseExact(from, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                toDate = DateTime.ParseExact(to, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                //limit of 31 days max
                TimeSpan limit = new TimeSpan(31, 0, 0, 0);
                TimeSpan requestedSpan = toDate - fromDate;
                if (requestedSpan > limit || fromDate > toDate)
                {
                    return BadRequest();
                }
            }
            else
            {
                //get latest price available in the database
                toDate = _context.Prices.Select(p => p.TransactionDateutc)
                    .OrderByDescending(p => p.Date)
                    .First();
                fromDate = toDate.AddDays(-daterange);
            }

            //get last 14 days of prices
            var prices = await _context.Prices
                .Where(p => p.FuelType == fueltype)
                .Where(p => p.TransactionDateutc < toDate)
                .Where(p => p.TransactionDateutc > fromDate)
                .ToListAsync();
            return prices;
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
        public async Task<ActionResult<IEnumerable<Prices>>> GetPrices(int id, string fueltype, string from, string to, int daterange = 31)
        {
            DateTime fromDate;
            DateTime toDate;

            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to))
            {
                fromDate = DateTime.ParseExact(from, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                toDate = DateTime.ParseExact(to, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);

                if (fromDate > toDate)
                {
                    return BadRequest();
                }
            }
            else
            {
                //get latest price available in the database
                toDate = _context.Prices.Select(p => p.TransactionDateutc)
                    .OrderByDescending(p => p.Date)
                    .First();
                fromDate = toDate.AddDays(-daterange);
            }

            //get last 14 days of prices
            var prices = await _context.Prices
                .Where(p => p.SiteId == id)
                .Where(p => p.FuelType == fueltype)
                .Where(p => p.TransactionDateutc < toDate)
                .Where(p => p.TransactionDateutc > fromDate)
                .ToListAsync();

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
