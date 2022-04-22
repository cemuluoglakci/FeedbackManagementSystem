using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetAdminFeedbackDetail
{
    public class GetAdminFeedbackDetailQuery : IRequest<BaseResponse<GetAdminFeedbackDetailVm>>
    {
        public int Id { get; set; }
    }
}
