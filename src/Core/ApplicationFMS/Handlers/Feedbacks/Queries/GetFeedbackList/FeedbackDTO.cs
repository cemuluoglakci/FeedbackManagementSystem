using ApplicationFMS.Helpers.Mappings;
using CoreFMS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetFeedbackList
{
    public class FeedbackDTO : IMapFrom<Feedback>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int? SectorId { get; set; }
        public string SectorName { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public int TypeId { get; set; }
        public string TypeName { get; set; }
        public int? SubTypeId { get; set; }
        public string SubTypeName { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}
