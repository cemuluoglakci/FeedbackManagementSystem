using ApplicationFMS.Models;
using System.Collections.Generic;

namespace ApplicationFMS.Handlers.Report.FeedbackCounts
{
    public class FeedbackCountsVm
    {
        public int TotalFeedbackCount { get; set; }
        public int AnonymFeedbackCount { get; set; }
        public int DirectedFeedbackCount { get; set; }
        public int RepliedFeedbackCount { get; set; }
        public int SolvedFeedbackCount { get; set; }
        public int ArchivedFeedbackCount { get; set; }
        public int TotalSharedCount { get; set; }
        public int TotalLikeCount { get; set; }
        public int TotalDislikeCount { get; set; }
        public List<StatisticalSubList>? FeedbacksPerProduct { get; set; }
        public List<StatisticalSubList>? FeedbacksPerType { get; set; }
    }

}
