using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleSolved
{
    public class ToggleSolvedFeedbackCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
