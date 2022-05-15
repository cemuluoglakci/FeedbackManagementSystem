using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleArchived
{
    public class ToggleArchivedFeedbackCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
