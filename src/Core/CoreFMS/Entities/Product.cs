using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class Product
    {
        public Product()
        {
            Feedbacks = new HashSet<Feedback>();
        }

        public int Id { get; set; }
        public int? CompanyId { get; set; }
        public string ProductName { get; set; } = null!;

        public virtual Company? Company { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
