using System;
using System.Collections.Generic;

namespace qldfuelanalyseapi.Models
{
    public partial class Prices
    {
        public int TransactionId { get; set; }
        public int SiteId { get; set; }
        public string FuelType { get; set; }
        public int Price { get; set; }
        public DateTime TransactionDateutc { get; set; }

        public virtual Sites Site { get; set; }
    }
}
