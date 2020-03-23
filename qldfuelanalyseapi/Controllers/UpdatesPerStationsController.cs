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
    public class UpdatesPerStationsController : ControllerBase
    {
        private readonly qldfuelContext _context;

        public UpdatesPerStationsController(qldfuelContext context)
        {
            _context = context;
        }

        // GET: api/UpdatesPerStations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UpdatesPerStation>>> GetUpdatesPerStation(int count = 10)
        {
            return await _context.UpdatesPerStation.Take(count).ToListAsync();
        }
    }
}
