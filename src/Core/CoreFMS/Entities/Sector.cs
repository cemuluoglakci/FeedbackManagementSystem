using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class Sector
    {
        public Sector()
        {
            Companies = new HashSet<Company>();
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public string SectorName { get; set; } = null!;

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
