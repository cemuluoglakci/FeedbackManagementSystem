using FluentValidation;

namespace ApplicationFMS.Handlers.Replies.Commands.DeleteReply
{
    public class DeleteReplyCommandValidator : AbstractValidator<DeleteReplyCommand>
    {
        public DeleteReplyCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
