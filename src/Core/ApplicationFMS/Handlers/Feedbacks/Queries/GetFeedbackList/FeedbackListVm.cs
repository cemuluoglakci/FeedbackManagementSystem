using ApplicationFMS.Handlers.UserHandlers.Queries.GetUserList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetFeedbackList
{
    public class FeedbackListVm
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public int ObjectsPerPage { get; set; }
        public int PageNumber { get; set; }
        public IList<UserDTO> FeedbackList { get; set; }
    }
}
