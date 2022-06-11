using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Replies.Commands.ToggleChecked
{
    public class ToggleCheckedReplyCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
