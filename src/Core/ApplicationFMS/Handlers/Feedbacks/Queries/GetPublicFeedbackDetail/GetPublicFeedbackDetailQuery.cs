using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail
{
    public class GetPublicFeedbackDetailQuery : IRequest<BaseResponse<GetPublicFeedbackDetailVm>>
    {
        public int Id { get; set; }
    }
}
