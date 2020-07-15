using System;
using System.Collections.Generic;

namespace qldfuelanalyseapi.Models
{
    public partial class Region
    {
        public Region()
        {
            InverseRegionParent = new HashSet<Region>();
            SiteRegion = new HashSet<SiteRegion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? OriginalId { get; set; }
        public int? GeographicalLevel { get; set; }
        public string Abbreviation { get; set; }
        public int? RegionParentId { get; set; }
        public bool? Active { get; set; }

        public virtual Region RegionParent { get; set; }
        public virtual ICollection<Region> InverseRegionParent { get; set; }
        public virtual ICollection<SiteRegion> SiteRegion { get; set; }
    }
}
