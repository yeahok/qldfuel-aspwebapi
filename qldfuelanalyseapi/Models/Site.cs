using System;
using System.Collections.Generic;

namespace qldfuelanalyseapi.Models
{
    public partial class Site
    {
        public Site()
        {
            Price = new HashSet<Price>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? BrandId { get; set; }
        public string Address { get; set; }
        public int? RegionLevel1Id { get; set; }
        public int? RegionLevel2Id { get; set; }
        public int? RegionLevel3Id { get; set; }
        public int? RegionLevel4Id { get; set; }
        public int? RegionLevel5Id { get; set; }
        public string PostCode { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Region RegionLevel1 { get; set; }
        public virtual Region RegionLevel2 { get; set; }
        public virtual Region RegionLevel3 { get; set; }
        public virtual Region RegionLevel4 { get; set; }
        public virtual Region RegionLevel5 { get; set; }
        public virtual ICollection<Price> Price { get; set; }
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
