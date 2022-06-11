using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Reactions.Commands.DeleteCommentReaction
{
    public class DeleteCommentReactionCommand : IRequest<BaseResponse>
    {
        public int CommentId { get; set; }
    }
}
