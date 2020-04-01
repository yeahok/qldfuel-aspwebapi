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
    public class SitesController : ControllerBase
    {
        private readonly qldfuelContext _context;

        public SitesController(qldfuelContext context)
        {
            _context = context;
        }

        // GET: api/Sites
        [HttpGet]
        public async Task<ActionResult<SitesObj>> GetSites(int limit = 10, int page = 1)
        {            
            var sites =  await _context.Sites
                .Skip(limit * (page - 1))
                .Take(limit)
                .ToListAsync();
            SitesObj sitesobj = new SitesObj();
            sitesobj.Sites = sites;

            int sitesCount = await _context.Sites.CountAsync();

            sitesobj.QueryInfo.RowCount = sitesCount;

            return sitesobj;
        }

        // GET: api/Sites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SitesObj>> GetSite(int id)
        {
            var site = await _context.Sites.FindAsync(id);

            if (site == null)
            {
                return NotFound();
            }

            SitesObj sitesobj = new SitesObj();
            sitesobj.Sites.Add(site);

            var fuelTypes = _context.Prices.Where(p => p.SiteId == id).Select(e => e.FuelType).Distinct();

            sitesobj.QueryInfo.FuelTypes = fuelTypes.ToList();

            return sitesobj;
        }

        private bool SitesExists(int id)
        {
            return _context.Sites.Any(e => e.SiteId == id);
        }
    }
}
