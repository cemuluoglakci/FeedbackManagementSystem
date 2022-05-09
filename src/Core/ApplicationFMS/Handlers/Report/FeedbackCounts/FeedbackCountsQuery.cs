using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Report.FeedbackCounts
{
    public class FeedbackCountsQuery : IRequest<BaseResponse<FeedbackCountsVm>>
    {
        public int ProductId { get; set; }
        public int TypeId { get; set; }
    }
}
