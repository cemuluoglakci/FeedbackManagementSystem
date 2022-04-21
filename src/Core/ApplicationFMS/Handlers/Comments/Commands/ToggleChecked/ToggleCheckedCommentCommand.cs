using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Comments.Commands.ToggleChecked
{
    public class ToggleCheckedCommentCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
    }
}
