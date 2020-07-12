using System;
using System.Collections.Generic;

namespace qldfuelanalyseapi.Models
{
    public partial class Site
    {
        public Site()
        {
            Price = new HashSet<Price>();
            SiteFuel = new HashSet<SiteFuel>();
            SiteRegion = new HashSet<SiteRegion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? BrandId { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? Active { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual ICollection<Price> Price { get; set; }
        public virtual ICollection<SiteFuel> SiteFuel { get; set; }
        public virtual ICollection<SiteRegion> SiteRegion { get; set; }
    }

    public class SiteView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string Brand { get; set; }
        public string RegionLevel1 { get; set; }
        public string RegionLevel2 { get; set; }
    }

    public class SiteMapView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public virtual IEnumerable<Price> Prices { get; set; }
    }

    public partial class SitesObj
    {
        public SitesObj()
        {
            Sites = new List<SiteView>();
            QueryInfo = new QueryInfo();
        }

        public List<SiteView> Sites { get; set; }

        public QueryInfo QueryInfo { get; set; }
    }

    public partial class QueryInfo
    {
        public int RowCount { get; set; }
        public List<String> FuelTypes { get; set; }
    }

    public enum ColumnSort
    {
        Id,
        Name,
        Brand,
        RegionLevel1
    }
}
