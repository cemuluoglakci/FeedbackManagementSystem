using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Reactions.Commands.DeleteCommentReaction
{
    public class DeleteCommentReactionCommand : IRequest<BaseResponse<int>>
    {
        public int CommentId { get; set; }
    }
}
