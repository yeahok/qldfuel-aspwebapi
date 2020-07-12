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
        public async Task<ActionResult<IEnumerable<PriceView>>> GetPrice(string fueltype, string from, string to, int daterange = 14)
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
                toDate = _context.Price.Select(p => p.TransactionDate)
                    .OrderByDescending(q => q.Date)
                    .First();
                fromDate = toDate.AddDays(-daterange);
            }

            //get last 14 days of prices
            var prices = await _context.Price
                .Select(p => new PriceView
                {
                    Id = p.Id,
                    SiteId = p.SiteId,
                    FuelName = p.Fuel.Name,
                    Amount = p.Amount,
                    TransactionDate = p.TransactionDate
                })
                .Where(p => p.FuelName == fueltype)
                .Where(p => p.TransactionDate < toDate)
                .Where(p => p.TransactionDate > fromDate)
                .ToListAsync();
            
            return prices;
        }

        [HttpGet("Latest/{id}")]
        public async Task<ActionResult<List<PriceView>>> GetLatestPrices(int id)
        {
            var prices = new List<PriceView>();
            var fuelTypes = await _context.Price.Where(p => p.SiteId == id).Select(e => e.Fuel).Distinct().ToListAsync();
            foreach (Fuel fuel in fuelTypes)
            {
                var priceView = await _context.Price
                    .Select(p => new PriceView
                    {
                        Id = p.Id,
                        SiteId = p.SiteId,
                        FuelName = p.Fuel.Name,
                        Amount = p.Amount,
                        TransactionDate = p.TransactionDate
                    })
                    .Where(p => p.SiteId == id && p.FuelName == fuel.Name)
                    .OrderByDescending(s => s.TransactionDate).Take(1).SingleAsync();
                prices.Add(priceView);
            }
            return prices;
        }

        // GET: api/Prices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PriceView>>> GetPrice(int id, string fueltype, string from, string to, int daterange = 31)
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
                toDate = _context.Price.Select(p => p.TransactionDate)
                    .OrderByDescending(p => p.Date)
                    .First();
                fromDate = toDate.AddDays(-daterange);
            }

            //get last 14 days of prices
            var prices = await _context.Price
                .Select(p => new PriceView
                {
                    Id = p.Id,
                    SiteId = p.SiteId,
                    FuelName = p.Fuel.Name,
                    Amount = p.Amount,
                    TransactionDate = p.TransactionDate
                })
                .Where(p => p.SiteId == id)
                .Where(p => p.FuelName == fueltype)
                .Where(p => p.TransactionDate < toDate)
                .Where(p => p.TransactionDate > fromDate)
                .ToListAsync();

            if (prices == null)
            {
                return NotFound();
            }

            return prices;
        }

        private bool PriceExists(int id)
        {
            return _context.Price.Any(e => e.Id == id);
        }
    }
}
