using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.DirectFeedback
{
    public class DirectFeedbackCommand : IRequest<BaseResponse>
    {
        public int FeedbackId { get; set; }
        public int EmployeeId { get; set; }
    }
}
