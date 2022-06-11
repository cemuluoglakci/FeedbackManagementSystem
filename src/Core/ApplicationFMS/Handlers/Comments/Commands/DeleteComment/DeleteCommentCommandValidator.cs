using FluentValidation;

namespace ApplicationFMS.Handlers.Comments.Commands.DeleteComment
{
    public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
    {
        public DeleteCommentCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }

    }
}
