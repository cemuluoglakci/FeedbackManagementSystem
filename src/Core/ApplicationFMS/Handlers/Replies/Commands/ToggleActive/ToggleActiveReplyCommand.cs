using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Replies.Commands.ToggleActive
{
    public class ToggleActiveReplyCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
    }
}
