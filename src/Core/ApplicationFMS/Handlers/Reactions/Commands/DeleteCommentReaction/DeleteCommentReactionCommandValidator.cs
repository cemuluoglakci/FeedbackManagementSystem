using FluentValidation;

namespace ApplicationFMS.Handlers.Reactions.Commands.DeleteCommentReaction
{
    public class DeleteCommentReactionCommandValidator : AbstractValidator<DeleteCommentReactionCommand>
    {
        public DeleteCommentReactionCommandValidator()
        {
            RuleFor(x => x.CommentId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
