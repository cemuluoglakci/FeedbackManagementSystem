using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetCompanyFeedbackDetail
{
    public class GetCompanyFeedbackDetailQuery : IRequest<BaseResponse<GetCompanyFeedbackDetailVm>>
    {
        public int Id { get; set; }
    }
}
