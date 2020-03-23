using System;
using System.Collections.Generic;

namespace qldfuelanalyseapi.Models
{
    public partial class UpdatesPerStation
    {
        public string SiteName { get; set; }
        public int? SiteId { get; set; }
        public long? Count { get; set; }
    }
}
