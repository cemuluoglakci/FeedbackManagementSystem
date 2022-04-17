using System.Collections.Generic;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList
{
    public class FeedbackListVm
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public int ObjectsPerPage { get; set; }
        public int PageNumber { get; set; }
        public IList<PublicFeedbackDTO>? PublicFeedbackList { get; set; }
        public IList<AdminFeedbackDTO>? AdminFeedbackList { get; set; }
        public IList<CompanyFeedbackDTO>? CompanyFeedbackList { get; set; }
    }
}
