using FluentValidation;

namespace ApplicationFMS.Handlers.Replies.Commands.ToggleActive
{
    public class ToggleActiveReplyCommandValidator : AbstractValidator<ToggleActiveReplyCommand>
    {
        public ToggleActiveReplyCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
