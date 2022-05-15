using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetAdminFeedbackDetail
{
    public class GetAdminFeedbackDetailQuery : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
