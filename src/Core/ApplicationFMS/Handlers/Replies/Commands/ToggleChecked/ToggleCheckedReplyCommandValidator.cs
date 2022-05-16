using FluentValidation;

namespace ApplicationFMS.Handlers.Replies.Commands.ToggleChecked
{
    public class ToggleCheckedReplyCommandValidator : AbstractValidator<ToggleCheckedReplyCommand>
    {
        public ToggleCheckedReplyCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
