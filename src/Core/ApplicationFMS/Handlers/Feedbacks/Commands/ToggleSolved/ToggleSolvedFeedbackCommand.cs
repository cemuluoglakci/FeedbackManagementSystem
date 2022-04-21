using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleSolved
{
    public class ToggleSolvedFeedbackCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
    }
}
