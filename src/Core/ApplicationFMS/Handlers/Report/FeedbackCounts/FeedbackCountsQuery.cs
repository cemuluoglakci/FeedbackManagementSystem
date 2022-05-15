using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Report.FeedbackCounts
{
    public class FeedbackCountsQuery : IRequest<BaseResponse>
    {
        public int ProductId { get; set; }
        public int TypeId { get; set; }
    }
}
