using FluentValidation;

namespace ApplicationFMS.Handlers.Reactions.Commands.ReactComment
{
    public class ReactCommentCommandValidator : AbstractValidator<ReactCommentCommand>
    {
        public ReactCommentCommandValidator()
        {
            RuleFor(x => x.CommentId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
            RuleFor(x => x.Sentiment).NotNull().NotEmpty();
        }
    }
}
