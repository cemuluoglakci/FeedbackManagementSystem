using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Replies.Commands.ToggleChecked
{
    public class ToggleCheckedReplyCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
    }
}
