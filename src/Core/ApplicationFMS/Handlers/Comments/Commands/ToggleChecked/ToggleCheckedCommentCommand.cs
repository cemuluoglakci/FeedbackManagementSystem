using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Comments.Commands.ToggleChecked
{
    public class ToggleCheckedCommentCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
