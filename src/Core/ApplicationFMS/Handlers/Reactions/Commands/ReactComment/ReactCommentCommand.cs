using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Reactions.Commands.ReactComment
{
    public class ReactCommentCommand : IRequest<BaseResponse>
    {
        public int CommentId { get; set; }
        public bool Sentiment { get; set; }
    }
}
