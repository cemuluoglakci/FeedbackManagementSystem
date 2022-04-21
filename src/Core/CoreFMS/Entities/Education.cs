using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class Education
    {
        public Education()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? EducationName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
