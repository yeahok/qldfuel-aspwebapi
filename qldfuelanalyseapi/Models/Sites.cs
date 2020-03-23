using System;
using System.Collections.Generic;

namespace qldfuelanalyseapi.Models
{
    public partial class Sites
    {
        public Sites()
        {
            Prices = new HashSet<Prices>();
        }

        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteBrand { get; set; }
        public string SitesAddressLine1 { get; set; }
        public string SiteSuburb { get; set; }
        public string SiteState { get; set; }
        public int? SitePostCode { get; set; }
        public decimal? SiteLatitude { get; set; }
        public decimal? SiteLongitude { get; set; }

        public virtual ICollection<Prices> Prices { get; set; }
    }
}
