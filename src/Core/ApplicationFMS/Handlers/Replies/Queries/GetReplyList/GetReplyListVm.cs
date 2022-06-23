using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using System.Collections.Generic;

namespace ApplicationFMS.Handlers.Replies.Queries.GetReplyList
{
    public class GetReplyListVm
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public int ObjectsPerPage { get; set; }
        public int PageNumber { get; set; }
        public IList<ReplyDto>? ReplyList { get; set; }

    }
}
