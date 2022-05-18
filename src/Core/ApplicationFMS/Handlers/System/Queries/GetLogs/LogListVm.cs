using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using System.Collections.Generic;

namespace ApplicationFMS.Handlers.System.Queries.GetLogs
{
    public class LogListVm
    {
        public int TotalCount { get; set; }
        public int FilteredCount { get; set; }
        public int ObjectsPerPage { get; set; }
        public int PageNumber { get; set; }
        public IList<LogDto>? LogList { get; set; }
    }
}
