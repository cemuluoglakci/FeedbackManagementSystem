using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Comments.Commands.ToggleActive
{
    public class ToggleActiveCommentCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
