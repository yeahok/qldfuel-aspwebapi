using System;
using System.Collections.Generic;

namespace qldfuelanalyseapi.Models
{
    public partial class Region
    {
        public Region()
        {
            InverseRegionParent = new HashSet<Region>();
            SiteRegionLevel1 = new HashSet<Site>();
            SiteRegionLevel2 = new HashSet<Site>();
            SiteRegionLevel3 = new HashSet<Site>();
            SiteRegionLevel4 = new HashSet<Site>();
            SiteRegionLevel5 = new HashSet<Site>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? OriginalId { get; set; }
        public int? GeographicalLevel { get; set; }
        public string Abbrevation { get; set; }
        public int? RegionParentId { get; set; }

        public virtual Region RegionParent { get; set; }
        public virtual ICollection<Region> InverseRegionParent { get; set; }
        public virtual ICollection<Site> SiteRegionLevel1 { get; set; }
        public virtual ICollection<Site> SiteRegionLevel2 { get; set; }
        public virtual ICollection<Site> SiteRegionLevel3 { get; set; }
        public virtual ICollection<Site> SiteRegionLevel4 { get; set; }
        public virtual ICollection<Site> SiteRegionLevel5 { get; set; }
    }
}
