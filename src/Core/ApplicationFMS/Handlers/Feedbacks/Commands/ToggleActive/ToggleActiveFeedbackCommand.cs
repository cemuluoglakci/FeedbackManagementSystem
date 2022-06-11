using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleActive
{
    public class ToggleActiveFeedbackCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }


}
