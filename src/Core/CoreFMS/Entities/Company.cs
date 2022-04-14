using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class Company
    {
        public Company()
        {
            Feedbacks = new HashSet<Feedback>();
            Products = new HashSet<Product>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public int? SectorId { get; set; }
        public string CompanyName { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public virtual Sector? Sector { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
