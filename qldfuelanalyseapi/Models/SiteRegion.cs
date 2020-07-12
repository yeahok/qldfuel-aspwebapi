using System;
using System.Collections.Generic;

namespace qldfuelanalyseapi.Models
{
    public partial class SiteRegion
    {
        public int Id { get; set; }
        public int? SiteId { get; set; }
        public int? RegionId { get; set; }

        public virtual Region Region { get; set; }
        public virtual Site Site { get; set; }
    }
}
