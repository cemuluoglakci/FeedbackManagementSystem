using System;
using System.Collections.Generic;

namespace CoreFMS.Entities
{
    public partial class SocialMedium
    {
        public SocialMedium()
        {
            Shares = new HashSet<Share>();
        }

        public int Id { get; set; }
        public string SocialMediaName { get; set; } = null!;

        public virtual ICollection<Share> Shares { get; set; }
    }
}
