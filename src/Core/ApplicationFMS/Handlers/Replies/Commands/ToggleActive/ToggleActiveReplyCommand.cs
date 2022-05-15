using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Replies.Commands.ToggleActive
{
    public class ToggleActiveReplyCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
