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
        public async Task<ActionResult<IEnumerable<Sites>>> GetSites(int count = 10)
        {
            return await _context.Sites.Take(count).ToListAsync();
        }

        // GET: api/Sites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sites>> GetSite(int id)
        {
            var sites = await _context.Sites.FindAsync(id);

            if (sites == null)
            {
                return NotFound();
            }

            return sites;
        }

        private bool SitesExists(int id)
        {
            return _context.Sites.Any(e => e.SiteId == id);
        }
    }
}
