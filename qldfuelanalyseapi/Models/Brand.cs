using System;
using System.Collections.Generic;

namespace qldfuelanalyseapi.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Site = new HashSet<Site>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Site> Site { get; set; }
    }

    public class BrandView
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
