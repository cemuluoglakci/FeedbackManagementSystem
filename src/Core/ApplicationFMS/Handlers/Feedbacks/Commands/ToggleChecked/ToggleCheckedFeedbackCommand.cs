using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleChecked
{
    public class ToggleCheckedFeedbackCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
    }
}
