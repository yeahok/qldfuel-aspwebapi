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
        public async Task<ActionResult<SitesObj>> GetSite(string search, string postcode, int brand, int limit = 10, int page = 1, int sortby = 0)
        {
            var sites = _context.Site.AsQueryable();

            if (brand != 0)
            {
                sites = sites.Where(s => s.BrandId == brand);
            }            

            if (!string.IsNullOrEmpty(search))
            {
                sites = sites.Where(s => s.Name.ToLower().Contains(search.ToLower()));
            }

            if (!string.IsNullOrEmpty(postcode))
            {
                sites = sites.Where(s => s.PostCode.Contains(postcode));
            }

            var siteView = sites.Select(s => new SiteView
            {
                Id = s.Id,
                Name = s.Name,
                Brand = s.Brand.Name,
                Address = s.Address,
                PostCode = s.PostCode,
                Latitude = s.Latitude,
                Longitude = s.Longitude,
                RegionLevel1 = s.SiteRegion.Where(sr => sr.Region.GeographicalLevel == 1).Select(sr => sr.Region.Name).First(),
                RegionLevel2 = s.SiteRegion.Where(sr => sr.Region.GeographicalLevel == 2).Select(sr => sr.Region.Name).First(),
                ModifiedDate = s.ModifiedDate,
            });

            ColumnSort column = new ColumnSort();

            if (sortby < 0 || sortby > 3)
            {
                return BadRequest();
            }
            else
            {
                column = (ColumnSort)sortby;
            }
            siteView = siteView.OrderBy(s => EF.Property<SiteView>(s, column.ToString()));

            SitesObj sitesObj = new SitesObj();
            sitesObj.QueryInfo.RowCount = await siteView.CountAsync();

            sitesObj.Sites = await siteView
                .Skip(limit * (page - 1))
                .Take(limit)
                .ToListAsync();

            return sitesObj;
        }

        // GET: api/Sites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SitesObj>> GetSite(int id)
        {
            var siteView = _context.Site.Select(s => new SiteView
            {
                Id = s.Id,
                Name = s.Name,
                Brand = s.Brand.Name,
                Address = s.Address,
                PostCode = s.PostCode,
                Latitude = s.Latitude,
                Longitude = s.Longitude,
                RegionLevel1 = s.SiteRegion.Where(sr => sr.Region.GeographicalLevel == 1).Select(sr => sr.Region.Name).First(),
                RegionLevel2 = s.SiteRegion.Where(sr => sr.Region.GeographicalLevel == 2).Select(sr => sr.Region.Name).First(),
                ModifiedDate = s.ModifiedDate,
            });

            var siteCheck = siteView.Where(s => s.Id == id);

            var site = new SiteView();

            if (siteCheck.Count() == 0)
            {
                return NotFound();
            }
            else
            {
                site = await siteCheck.FirstAsync();
            }            

            SitesObj sitesobj = new SitesObj();
            sitesobj.Sites.Add(site);

            var fuelTypes = _context.Price.Where(p => p.SiteId == id).Select(f => f.Fuel.Name).Distinct();

            sitesobj.QueryInfo.FuelTypes = fuelTypes.ToList();

            return sitesobj;
        }

        [HttpGet("MapData")]
        public async Task<ActionResult<List<SiteMapView>>> GetMapData(string fueltype)
        {
            var sites = await _context.Site
                .Where(c => c.Price.Any(p => p.Fuel.Name == fueltype))
                .Select(e => new SiteMapView
                {
                    Id = e.Id,
                    Name = e.Name,
                    Latitude = e.Latitude,
                    Longitude = e.Longitude,
                    Prices = e.Price.Where(p => p.Fuel.Name == fueltype)
                                    .OrderByDescending(o => o.TransactionDate).Take(1)
                })
                .ToListAsync();

            return sites;
        }

        private bool SiteExists(int id)
        {
            return _context.Site.Any(e => e.Id == id);
        }
    }
}
