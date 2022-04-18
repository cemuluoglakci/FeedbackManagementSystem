using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleActive
{
    public class ToggleActiveFeedbackCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
    }


}
