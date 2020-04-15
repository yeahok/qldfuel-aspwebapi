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

        public virtual IEnumerable<Prices> Prices { get; set; }
    }

    public enum ColumnSort
    {
        SiteId,
        SiteName
    }

    public partial class SitesObj
    {
        public SitesObj() {
            Sites = new List<Sites>();
            QueryInfo = new QueryInfo();
        }

        public List<Sites> Sites { get; set; }

        public QueryInfo QueryInfo { get; set; }
    }

    public partial class QueryInfo
    {
        public int RowCount { get; set; }
        public List<string> FuelTypes { get; set; }
    }
}
