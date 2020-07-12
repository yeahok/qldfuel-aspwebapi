using System;
using System.Collections.Generic;

namespace qldfuelanalyseapi.Models
{
    public partial class Price
    {
        public int Id { get; set; }
        public int? SiteId { get; set; }
        public int? FuelId { get; set; }
        public string CollectionMethod { get; set; }
        public int? Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public bool? Active { get; set; }

        public virtual Fuel Fuel { get; set; }
        public virtual Site Site { get; set; }
    }

    public class PriceView
    {
        public int Id { get; set; }
        public int? SiteId { get; set; }
        public string FuelName { get; set; }
        public int? Amount { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
