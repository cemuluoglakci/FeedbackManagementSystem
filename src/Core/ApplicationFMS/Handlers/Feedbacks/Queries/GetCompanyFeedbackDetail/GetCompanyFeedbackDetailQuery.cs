using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetCompanyFeedbackDetail
{
    public class GetCompanyFeedbackDetailQuery : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
