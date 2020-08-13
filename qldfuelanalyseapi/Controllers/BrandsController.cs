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
    public class BrandsController : ControllerBase
    {
        private readonly qldfuelContext _context;

        public BrandsController(qldfuelContext context)
        {
            _context = context;
        }

        // GET: api/Brands
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BrandView>>> GetBrand()
        {
            var brands = await _context.Brand
                .Where(b => b.Active == true)
                .Select(b => new BrandView
                {
                    Id = b.Id,
                    Name = b.Name
                }).ToListAsync();
            
            return brands;
        }

        // GET: api/Brands/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brand>> GetBrand(int id)
        {
            var brand = await _context.Brand.FindAsync(id);

            if (brand == null)
            {
                return NotFound();
            }

            return brand;
        }

        private bool BrandExists(int id)
        {
            return _context.Brand.Any(e => e.Id == id);
        }
    }
}
