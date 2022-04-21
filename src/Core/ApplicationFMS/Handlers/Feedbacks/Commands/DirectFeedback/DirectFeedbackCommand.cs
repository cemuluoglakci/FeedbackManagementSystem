using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.DirectFeedback
{
    public class DirectFeedbackCommand : IRequest<BaseResponse<int>>
    {
        public int FeedbackId { get; set; }
        public int EmployeeId { get; set; }
    }
}
