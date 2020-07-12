using System;
using System.Collections.Generic;

namespace qldfuelanalyseapi.Models
{
    public partial class SiteFuel
    {
        public int Id { get; set; }
        public int? SiteId { get; set; }
        public int? FuelId { get; set; }
        public bool? Active { get; set; }

        public virtual Site Site { get; set; }
    }
}
