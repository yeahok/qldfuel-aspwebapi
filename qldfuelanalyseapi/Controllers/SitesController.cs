using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
        public async Task<ActionResult<SitesObj>> GetSites(string search, string brand, int limit = 10, int page = 1, int sortby = 0)
        {
            var column = (ColumnSort)sortby;

            var sites = _context.Sites.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                sites = sites.Where(s => s.SiteName.ToLower().Contains(search.ToLower()));
            }
            if (!string.IsNullOrEmpty(brand))
            {
                sites = sites.Where(s => s.SiteBrand == brand);
            }
            sites = sites.OrderBy(c => EF.Property<Sites>(c, column.ToString()));

            SitesObj sitesobj = new SitesObj();
            sitesobj.Sites = await sites
                .Skip(limit * (page - 1))
                .Take(limit)
                .ToListAsync();

            int sitesCount = await sites.CountAsync();

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

        [HttpGet("MapData")]
        public async Task<ActionResult<List<Sites>>> GetMapData(string fueltype)
        {
            var sites = await _context.Sites
                .Where(c => c.Prices.Any(p => p.FuelType == fueltype))
                .Select(e => new Sites{
                    SiteId = e.SiteId,
                    SiteName = e.SiteName,
                    SiteBrand = e.SiteBrand,
                    SitesAddressLine1 = e.SitesAddressLine1,
                    SiteState = e.SiteState,
                    SitePostCode = e.SitePostCode,
                    SiteLatitude = e.SiteLatitude,
                    SiteLongitude = e.SiteLongitude,
                    Prices = e.Prices.Where(p => p.FuelType== fueltype)
                                    .OrderByDescending(o => o.TransactionDateutc).Take(1)
                })
                .ToListAsync();

            return sites;
        }

        [HttpGet("Brands")]
        public async Task<ActionResult<List<string>>> GetBrands()
        {
            var brands = await _context.Sites.Select(s => s.SiteBrand).Distinct().ToListAsync();
            return brands.OrderBy(b => b).ToList();
        }

        private bool SitesExists(int id)
        {
            return _context.Sites.Any(e => e.SiteId == id);
        }
    }
}
