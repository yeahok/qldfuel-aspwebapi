using System;
using System.Collections.Generic;

namespace qldfuelanalyseapi.Models
{
    public partial class Fuel
    {
        public Fuel()
        {
            Price = new HashSet<Price>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<Price> Price { get; set; }
    }
}
